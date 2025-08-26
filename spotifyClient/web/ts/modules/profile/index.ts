import { KEY_ACCESS_TOKEN, KEY_PROFILE_OBJECT, SPOTIFY_API_CONTENT_TYPE, SPOTIFY_API_URL_GET_CURENT_USER_PROFILE } from "../../shared/constants.js";
import type { UserProfile } from "../../state/types.js";

export class Profile {
    async getCurrentUserProfile() {
        let userProfile = localStorage.getItem(KEY_PROFILE_OBJECT);
        if (userProfile) {
            this.#renderUserProfile(JSON.parse(userProfile));
        }
        else {
            userProfile = await this.#getCurrentUserProfile();
        }
    }

    #renderUserProfile(userProfile: UserProfile) {
        const btnUserProfile: HTMLButtonElement = document.querySelector("#btnUserProfile")!;
        // console.log(userProfile, btnUserProfile);
        btnUserProfile.style.backgroundImage = `url(${userProfile.images[1]?.url})`;
        
    }

    async #getCurrentUserProfile() {
        const accessToken = localStorage.getItem(KEY_ACCESS_TOKEN);
        if (!accessToken) {
            return null;
        }

        const payload = {
            method: "GET",
            headers: {
                "Content-Type": SPOTIFY_API_CONTENT_TYPE,
                "Authorization": `Bearer ${accessToken}`
            },
        };

        const body = await fetch(SPOTIFY_API_URL_GET_CURENT_USER_PROFILE, payload);
        const response = await body.json();
        localStorage.setItem(KEY_PROFILE_OBJECT, JSON.stringify(response));

        return JSON.stringify(response);
    }
}