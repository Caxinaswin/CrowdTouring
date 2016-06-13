if (!!navigator.geolocation) {
    //para visualiação   
    function initMapView() {
        var latlng;
        var geolocate;
        var map;
        var lat = document.getElementById("lat2").value;
        var lon = document.getElementById("lon2").value;
        var directionsService = new google.maps.DirectionsService;
        var directionsDisplay = new google.maps.DirectionsRenderer;
        latlng = new google.maps.LatLng(lat, lon);
        map = new google.maps.Map(document.getElementById('mapaLocal'), {

            center: latlng,
            zoom: 6
        });

        directionsDisplay.setMap(map);

        var marker = new google.maps.Marker({
            position: latlng,
            map: map,
            draggable: false,
            title: "Drag me!"
        });

        var contentString = '<p>Hi you are here at the moment!</p>';

        var infowindow = new google.maps.InfoWindow({
            content: contentString
        });

        marker.addListener('click', function () {
            infowindow.open(map, marker);
        });

        navigator.geolocation.getCurrentPosition(function (position) {

             geolocate = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);


            var markerUtilizador = new google.maps.Marker({
                position: geolocate,
                map: map,
                draggable: false,
                title: "Drag me!"
            });

            calculateAndDisplayRoute(directionsService,directionsDisplay)
        });
       
        function calculateAndDisplayRoute(directionsService, directionsDisplay) {
            directionsService.route({
                origin: geolocate,
                destination: latlng,
                travelMode: google.maps.TravelMode.DRIVING
            }, function (response, status) {
                if (status === google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(response);
                } else {
                    window.alert('Directions request failed due to ' + status);
                }
            });
        }

    }
}
else
{
    document.getElementById('google_canvas').innerHTML = 'No Geolocation Support.';

}

