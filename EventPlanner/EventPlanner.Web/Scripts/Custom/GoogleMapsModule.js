function initialize() {
    var mapCanvas = document.getElementById('map');

    var mapOptions = {
        center: new google.maps.LatLng(49.197947, 16.607823),
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }

    var map = new google.maps.Map(mapCanvas, mapOptions)
}

google.maps.event.addDomListener(window, 'load', initialize);
