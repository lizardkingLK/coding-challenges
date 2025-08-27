import { RANDOM_STRING_INPUT } from "../../shared/constants.js";

export class CryptographyHelper {
  generateRandomString(length: number) {
    const randomValues = crypto.getRandomValues(new Uint8Array(length));
    const randomStringSize = RANDOM_STRING_INPUT.length;

    return randomValues.reduce(
      (acc: string, x: number): string =>
        acc + RANDOM_STRING_INPUT[x % randomStringSize],
      ""
    );
  }

  async getSHA256String(plain: string) {
    const encoder = new TextEncoder();
    const data = encoder.encode(plain);

    return window.crypto.subtle.digest("SHA-256", data);
  }

  generateBase64Encode(input: ArrayBuffer) {
    return btoa(String.fromCharCode(...new Uint8Array(input)))
      .replace(/=/g, "")
      .replace(/\+/g, "-")
      .replace(/\//g, "_");
  }
}
