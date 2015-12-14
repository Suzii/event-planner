var selectedItem;
// initialize map and places list
function initialize() {
    var mapCanvas = document.getElementById('map');

    // set map options
    var mapOptions = {
        center: new google.maps.LatLng(49.197947, 16.607823),
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }

    // initialize map
    var map = new google.maps.Map(mapCanvas, mapOptions);
    
    // container for places list
    $("#map-container").append('<div id="places-list" class="col-md-3"><p class="place-header">Places to vote for:</p></div>');

    var markers = [];
    var places;
    var bounds = new google.maps.LatLngBounds();
    var infowindow = new google.maps.InfoWindow();
    var iconUrl = $("#map-container").attr("data-icon-image-url");
    
    // getting places from json
    $.getJSON($("#map-container").attr("data-places"), function (data) {
        places = data.Data;
        for (var i = 0; i < places.length; i++) {

            // add marker for place to map
            var marker = new google.maps.Marker({
                map: map,
                animation: google.maps.Animation.DROP,
                position: { lat: places[i].Lat, lng: places[i].Lng },
                icon: iconUrl
            });

            // fit markers to screen
            bounds.extend(new google.maps.LatLng(places[i].Lat, places[i].Lng));
            map.fitBounds(bounds);

            // open info window on click
            google.maps.event.addListener(marker, 'click', setInfoWindow(marker, i));
                        
            // bounce marker on place hover or click
            var placeItem = appendPlaceItem(marker, places[i].Name, places[i].AddressInfo);
            
            // highlight place item on marker hover
            google.maps.event.addListener(marker, 'mouseover', setPlaceItemHighlight(placeItem));
            google.maps.event.addListener(marker, 'mouseout', setPlaceItemHighlight(placeItem));

            // add marker to array
            markers.push(marker); 
        }       

    });

    function setInfoWindow(marker, i) {
        return function () {
            infowindow.setContent('<div class="text-primary header" style="font-weight: normal">' + places[i].Name + '</div>' +
            '<div class="text-muted">' + places[i].AddressInfo + '</div>');
            infowindow.open(map, marker);
        }
    };


    function appendPlaceItem(marker, name, address) {
        var clicked = false;
        return $('<div class="place-item">' + name + '<span>' + address + '</span></div>')
            .hover(function (marker) {
                return function () {
                    if (selectedItem == null) {
                        marker.setAnimation(google.maps.Animation.BOUNCE);
                    }
                }
            }(marker), function (marker) {
                return function () {
                    if (selectedItem == null) {
                        marker.setAnimation(null);
                    }
                }
            }(marker))
            .click(function (marker) {
                return function () {
                    if (clicked) {
                        if (selectedItem == this) {
                            selectedItem = null;
                        }

                        marker.setAnimation(null);
                        $(this).removeClass("hover");
                        clicked = false;
                    }
                    else {
                        if (selectedItem != null && selectedItem != this) {
                            selectedItem.click();
                        }

                        marker.setAnimation(google.maps.Animation.BOUNCE);
                        $(this).addClass("hover");
                        marker.getMap().panTo(marker.getPosition());
                        clicked = true;
                        selectedItem = this;
                    }
                }
            }(marker))
            .appendTo($("#places-list"));
    };


    function setPlaceItemHighlight(placeItem) {
        return function () {
            placeItem.toggleClass("hover");
        }
    };
    
}

google.maps.event.addDomListener(window, 'load', initialize);