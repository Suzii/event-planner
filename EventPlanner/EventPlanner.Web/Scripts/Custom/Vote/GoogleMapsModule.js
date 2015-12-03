var places;

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

    var marker, i;
    var markers = [];
    var bounds = new google.maps.LatLngBounds();
    var infoWindow;

    $.getJSON($("#map-container").attr("data-places"), function (data) {
        places = data.Data;
        for (var i = 0; i < places.length; i++) {
            var marker = new google.maps.Marker({
                map: map,
                animation: google.maps.Animation.DROP,
                position: { lat: places[i].Lat, lng: places[i].Lng },
                icon: './../../../Content/Images/point.png'
            });

            // fit markers to screen
            bounds.extend(new google.maps.LatLng(places[i].Lat, places[i].Lng));
            map.fitBounds(bounds);

            // open info window on click
            var infowindow = new google.maps.InfoWindow();

            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                return function () {
                    infowindow.setContent('<div class="text-primary header" style="font-weight: normal">' + places[i].Name + '</div>' +
                    '<div class="text-muted">' + places[i].AddressInfo + '</div>');
                    infowindow.open(map, marker);
                }
            })(marker, i));

            // bounce marker on place hover
            $('<div />').html(places[i].Name).addClass('text-primary').hover(function (marker) {
                return function () {
                    marker.setAnimation(google.maps.Animation.BOUNCE);
                }
            }(marker), function (marker) {
                return function () {
                    marker.setAnimation(null);
                }
            }(marker)).appendTo("#map-container");

            markers.push(marker); 
        }       

    });
}

google.maps.event.addDomListener(window, 'load', initialize);