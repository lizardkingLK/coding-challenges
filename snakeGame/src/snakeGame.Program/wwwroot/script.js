const apiUrl = "http://localhost:5000/events";
const eventSource = new EventSource(apiUrl);
const messages = document.querySelector("#messages");

eventSource.onmessage = ({ data }) => {
    messages.innerHTML += `<br/> ${data}`;
}
