export class WindowHelper {
  getSearchParams() {
    const searchString = window.location.search;
    const urlSearchParams = new URLSearchParams(searchString);

    return Object.fromEntries(urlSearchParams);
  }
}
