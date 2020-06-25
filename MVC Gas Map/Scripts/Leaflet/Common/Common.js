//------------------ADD SEARCH CONTROL  TO MAP OBJ
function addSearchControl(mapObj) {
    searchControl = L.esri.Geocoding.geosearch().addTo(mapObj);

    results = L.layerGroup().addTo(mapObj);

    searchControl.on('results', function (data) {
        results.clearLayers();
        for (var i = data.results.length - 1; i >= 0; i--) {
            results.addLayer(L.marker(data.results[i].latlng));
        }
    });

    return mapObj;
}

//------------------ADD SEARCH CONTROL  TO MAP OBJ
function addCoordBox(mapObj) {
    L.control.coordinates({
        position: "bottomleft",
        decimals: 0, //optional default 4
        decimalSeperator: ".", //optional default "."
        labelTemplateLat: "Latitude: {y}", //optional default "Lat: {y}"
        labelTemplateLng: "Longitude: {x}", //optional default "Lng: {x}"
        enableUserInput: true, //optional default true
        useDMS: false, //optional default false
        useLatLngOrder: true, //ordering of labels, default false-> lng-lat
        markerType: L.marker, //optional default L.marker
        markerProps: {} //optional default {}
    }).addTo(mapObj);

    return mapObj;
}


