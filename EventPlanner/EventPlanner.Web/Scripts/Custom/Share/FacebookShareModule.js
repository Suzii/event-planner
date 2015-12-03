(function (d, s, id) {
    var fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    var js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_GB/sdk.js#xfbml=1&version=v2.5";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));