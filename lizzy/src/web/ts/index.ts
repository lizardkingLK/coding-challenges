import { Authorization } from "./modules/authorization/index.js";
import { Profile } from "./modules/profile/index.js";

let authorization = null;
let profile = null;

window.onload = async function () {
  authorization ??= new Authorization();
  profile ??= new Profile();

  await authorization.authorize();
  await profile.getCurrentUserProfile();
};
