const darkButton = document.getElementById('dark-mode');
const lightButton = document.getElementById('light-mode');
const body = document.body; // won't work without 'defer' attribute

// apply cached on reload
const theme = localStorage.getItem('theme');

if (theme) {
    body.classList.add(theme);
}

// button event handlers
darkButton.onclick = () => {
    body.classList.replace('light-mode', 'dark-mode');
    localStorage.setItem('theme', 'dark-mode');
};

lightButton.onclick = () => {
    body.classList.replace('dark-mode', 'light-mode');
    localStorage.setItem('theme', 'light-mode');
};