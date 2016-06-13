var map;
document.getElementById("lat").value = 39.71;
document.getElementById("lon").value = -8.12;
function initMap() {
    var directionsService = new google.maps.DirectionsService;
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 39.71, lng: -8.12 },
        zoom: 6

    });


    // Place a draggable marker on the map
    var marker = new google.maps.Marker({
        position: { lat: 39.71, lng: -8.12 },
        map: map,
        draggable: true,
        title: "Drag me!"
    });

    marker.addListener('dragend', function (evt) {
        document.getElementById("lat").value = this.getPosition().lat();
        document.getElementById("lon").value = this.getPosition().lng();
        console.log(x);
    });

}




