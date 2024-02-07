function password() {
    var x = document.getElementById("pass_input");
    var y = document.getElementById("eye_btn");


    if (x.type === "password") {
        x.type = "text";
        y.src = "/Images/SRS Screen Shorts/eye-password-hide-svgrepo-com.svg";

    } else {
        x.type = "password";
        y.src = "/Images/SRS Screen Shorts/password_icon.svg";

    }
}


function darkMode() {

    // localStorage.setItem('isDarkMode', true);
    // if (localStorage.getItem('isDarkMode') === 'true') {

    //     if (document.documentElement.getAttribute('data-bs-theme') == 'dark') {
    //         document.documentElement.setAttribute('data-bs-theme','light')
    //     }
    //     else {
    //         document.documentElement.setAttribute('data-bs-theme','dark')
    //     }
    // }

    if (document.documentElement.getAttribute('data-bs-theme') == 'dark') {
        localStorage.setItem("PageTheme", "light");
        document.documentElement.setAttribute('data-bs-theme', 'light')
    }
    else {
        localStorage.setItem("PageTheme", "dark");
        document.documentElement.setAttribute('data-bs-theme', 'dark')
    }

}
if(localStorage.getItem("PageTheme") === "light"){
    document.documentElement.setAttribute('data-bs-theme','light')
}
else{
    document.documentElement.setAttribute('data-bs-theme','dark')
}


// function darkMode() {
//     localStorage.setItem('isDarkMode', true);
// }
// if (localStorage.getItem('isDarkMode') === 'true') {
//     document.getElementById('main-page').classList.add('active-dark');
// } 