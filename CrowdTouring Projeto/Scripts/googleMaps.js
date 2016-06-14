
function initMap() {
    var pos;
    var map = new google.maps.Map(document.getElementById('map'), {
        center: {lat: -34.397, lng: 150.644},
        zoom: 6
    });

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
                pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
                };

                var contentString = '<div style="width:150px" id="content">' +
                    '<p>' + "Você está aqui" + '</p>' + 
                    '<p>' + "Selecciona através do marcador o local do desafio"
                    '</div>';

                    var contentString2 = '<div id="content">' +
                        '<p>' + "Selecciona através do marcador o local do desafio"
                    '</div>';


            var marker = new google.maps.Marker({
                position: pos,
                map: map,
                title: 'Hello World!',
                draggable: true
            });

            marker.addListener('drag', function (event) {
                infowindow.setContent(contentString2)
            });

            marker.addListener('dragend', function (evt) {
                document.getElementById("lat").value = this.getPosition().lat();
                document.getElementById("lon").value = this.getPosition().lng();

            });

            var infowindow = new google.maps.InfoWindow();
                infowindow.setContent(contentString);
                infowindow.open(map, marker);


            map.setCenter(pos);

        }, function() {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
                          'Error: The Geolocation service failed.' :
                          'Error: Your browser doesn\'t support geolocation.');
}





