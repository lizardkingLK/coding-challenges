// Cryptography
export const CODE_VERIFIER_LENGTH = 64;
export const RANDOM_STRING_INPUT =
  "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

// App
export const APP_PORT = 5500;
export const APP_URL_HOME = `http://localhost:${APP_PORT}`;

// Keys - localStorage
export const KEY_CODE_VERIFIER = "code_verifier";
export const KEY_AUTH_TOKEN = "auth_token";
export const KEY_ACCESS_TOKEN = "access_token";
export const KEY_REFRESH_TOKEN = "refresh_token";
export const KEY_PROFILE_OBJECT = "current_user";

// APIs - Spotify
export const SPOTIFY_API_CLIENT_ID = "0ec6076d4098439483a2bdce8d426859";
export const SPOTIFY_API_GRANT_TYPE = "authorization_code";
export const SPOTIFY_API_CODE_CHALLENGE_METHOD = "S256";
export const SPOTIFY_API_SCOPE = "user-read-private user-read-email";
export const SPOTIFY_API_CONTENT_TYPE = "application/x-www-form-urlencoded";
export const SPOTIFY_API_URL_AUTHORIZE = "https://accounts.spotify.com/authorize";
export const SPOTIFY_API_URL_GET_TOKEN = "https://accounts.spotify.com/api/token";
export const SPOTIFY_API_URL_GET_CURENT_USER_PROFILE = "https://api.spotify.com/v1/me";

// Common
export const ERROR = "error";
export const CODE = "code";
