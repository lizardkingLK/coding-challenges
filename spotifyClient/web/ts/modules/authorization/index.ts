import { CryptographyHelper } from "../../helpers/cryptography/index.js";
import { WindowHelper } from "../../helpers/window/index.js";
import {
  APP_URL_HOME,
  CODE,
  CODE_VERIFIER_LENGTH, ERROR, KEY_ACCESS_TOKEN, KEY_AUTH_TOKEN, KEY_CODE_VERIFIER, KEY_REFRESH_TOKEN, SPOTIFY_API_CLIENT_ID, SPOTIFY_API_CODE_CHALLENGE_METHOD, SPOTIFY_API_CONTENT_TYPE, SPOTIFY_API_GRANT_TYPE, SPOTIFY_API_SCOPE, SPOTIFY_API_URL_AUTHORIZE, SPOTIFY_API_URL_GET_TOKEN
} from "../../shared/constants.js";

export class Authorization {
  #window: WindowHelper;
  #cryptography: CryptographyHelper;

  constructor() {
    this.#cryptography = new CryptographyHelper();
    this.#window = new WindowHelper();
  }

  async authorize() {
    if (!this.isAuthorized()) {
      await this.#authorize();
    }
  }

  isAuthorized() {
    return window.localStorage.getItem(KEY_AUTH_TOKEN) !== null;
  }

  async #authorize() {
    const searchParams = this.#window.getSearchParams();

    if (Object.hasOwn(searchParams, CODE)) {
      await this.#getAccessToken(searchParams[CODE] as string);
      window.location.href = APP_URL_HOME;
      return true;
    } else if (Object.hasOwn(searchParams, ERROR)) {
      console.error(searchParams[ERROR] as string);
      return false;
    } else if (localStorage.getItem(KEY_ACCESS_TOKEN) === null) {
      this.#getAuthorizationCode();
      return true;
    }
  }

  async #getAccessToken(authorizationCode: string) {
    const codeVerifier = localStorage.getItem(KEY_CODE_VERIFIER)!;

    const params = {
      client_id: SPOTIFY_API_CLIENT_ID,
      grant_type: SPOTIFY_API_GRANT_TYPE,
      code: authorizationCode,
      redirect_uri: APP_URL_HOME,
      code_verifier: codeVerifier,
    };

    const payload = {
      method: "POST",
      headers: {
        "Content-Type": SPOTIFY_API_CONTENT_TYPE,
      },
      body: new URLSearchParams(params).toString(),
    };

    const body = await fetch(SPOTIFY_API_URL_GET_TOKEN, payload);
    const response = await body.json();
    const { access_token, refresh_token } = response;

    localStorage.setItem(KEY_ACCESS_TOKEN, access_token);
    localStorage.setItem(KEY_REFRESH_TOKEN, refresh_token);
  }

  async #getAuthorizationCode() {
    const { codeVerifier, base64Value: codeChallenge } =
      await this.#initialize();

    const clientId = SPOTIFY_API_CLIENT_ID;

    window.localStorage.setItem(KEY_CODE_VERIFIER, codeVerifier);

    const params = {
      response_type: CODE,
      client_id: clientId,
      scope: SPOTIFY_API_SCOPE,
      code_challenge_method: SPOTIFY_API_CODE_CHALLENGE_METHOD,
      code_challenge: codeChallenge,
      redirect_uri: APP_URL_HOME,
    };

    const authUrl = new URL(SPOTIFY_API_URL_AUTHORIZE);
    authUrl.search = new URLSearchParams(params).toString();
    window.location.href = authUrl.toString();
  }

  async #initialize() {
    const codeVerifier =
      this.#cryptography.generateRandomString(CODE_VERIFIER_LENGTH);

    const sha256Value = await this.#cryptography.getSHA256String(codeVerifier);

    const base64Value = this.#cryptography.generateBase64Encode(sha256Value);

    return { codeVerifier, base64Value };
  }
}
