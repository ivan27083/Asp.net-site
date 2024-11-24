function setTheme() {
    if (localStorage.getItem('theme') == 'light') {
        document.body.classList.remove("light");
        document.body.classList.add("dark");
        window.localStorage.setItem('theme', 'dark')
    }
    else {
        document.body.classList.remove("dark");
        document.body.classList.add("light");
        window.localStorage.setItem('theme', 'light')
    }
}
function checkTheme() {
    const localStorageTheme = localStorage.getItem("theme");
    if (localStorageTheme !== null && localStorageTheme === "dark") {
        document.body.className = localStorageTheme;
    }
    else window.localStorage.setItem('theme', 'light');
}
window.onload = checkTheme();
$('#button-theme').on('click', setTheme);