type ExplicitContent = {
    filter_enabled: boolean;
    filter_locked: boolean;
}

type ExternalUrls = {
    spotify: string;
}

type Followers = {
    href: string;
    total: number;
}

export type ImageObject = {
    url: string;
    height: number;
    width: number;
}

export type UserProfile = {
    country: string;
    display_name: string;
    email: string;
    explicit_content: ExplicitContent;
    external_urls: ExternalUrls;
    followers: Followers;
    href: string;
    id: string;
    images: ImageObject[];
    product: string;
    type: string;
    uri: string;
}