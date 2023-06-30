const skinToggler = document.getElementById('skinToggler');

const toggleSkin = () => {
    document.body.classList.toggle('dark');
}

skinToggler.addEventListener('click', toggleSkin);
