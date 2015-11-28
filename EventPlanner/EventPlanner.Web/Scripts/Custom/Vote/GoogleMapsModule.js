function initialize() {
    var mapCanvas = document.getElementById('map');

    var mapOptions = {
        center: new google.maps.LatLng(49.197947, 16.607823),
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }

    var map = new google.maps.Map(mapCanvas, mapOptions);
    
    var bounds = new google.maps.LatLngBounds();

    map.data.addListener('addfeature', function (data) {
        bounds.extend(data.feature.getGeometry().get());
        map.fitBounds(bounds);
    });
    
    map.data.loadGeoJson($("#map").attr("data-places"));
        
    var infoWindow = new google.maps.InfoWindow();
    map.data.addListener('click', function (data) {
        infoWindow.setContent(
            '<div class="text-primary header" style="font-weight: normal">' + data.feature.getProperty('name') + '</div>' +
            '<div class="text-muted">' + data.feature.getProperty('address') + '</div>'
            );
        infoWindow.setPosition(data.feature.getGeometry().get());
        infoWindow.open(map);
    });

    map.data.setStyle({
        icon: './../../../Content/Images/point.png'
    });
}


google.maps.event.addDomListener(window, 'load', initialize);
