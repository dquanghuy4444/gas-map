// Variable
var lat = null;
var long = null;
var popupContent = null;
var defaultCoord = null;
var mapObj = null;
var marker;
var zoomLevel = 13;
var minimap = null;

//var amountOfStos = null;
var url = new URL(document.URL);
var search_params = url.searchParams;
var markerGroup = null;
var popup = L.popup();

var osmUrl = 'http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
var osmAttrib = 'Có làm mới có ăn !!! ';
var osm = new L.TileLayer(osmUrl, { minZoom: 5, maxZoom: 18, attribution: osmAttrib });
var osm2 = new L.TileLayer(osmUrl, { minZoom: 0, maxZoom: 13, attribution: osmAttrib });
///////////////////
//L.MakiMarkers.accessToken = 'pk.eyJ1IjoiZDNwemExMm81IiwiYSI6ImNrOWljamZqczE4M2EzcHFiY3NoZms2ZTYifQ.lhpwFvZyHvEy5fZFBBhXxw';


if (!search_params.has('lat') || !search_params.has('lng')) {
    defaultCoord = [21.061540, 105.781103];//toạ độ mặc địNH Ở PHẠM VĂN ĐỒNG
}
else {
    var latId = search_params.get('lat');
    var lngId = search_params.get('lng');
    defaultCoord = [latId, lngId];
}


// Window load
window.onload = function () {
    var mapConfig = {
        attributionControl: true, // hiện watermark
        center: defaultCoord, // vị trí map mặc định hiện tại
        zoom: zoomLevel, // level zoom
        fullscreenControl: true,
        fullscreenControlOptions: {
            position: 'topleft'
        }
    };


    mapObj = new L.map('idGasMap', mapConfig);

    mapObj.addLayer(osm);

    //
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

    mapObj.on('click', onMapClick);

    var layerGroup = new L.layerGroup([osm2]);
    minimap = new L.Control.MiniMap(layerGroup, { toggleDisplay: true }).addTo(mapObj);


    getAllStoresInDb();
    markerGroup = L.layerGroup().addTo(mapObj);

    setInterval(function () {
        markerGroup.clearLayers();
        getAllStoresInDb();
    }, 20000)

    ///
    var layers = [];
    for (var providerId in providers) {
        layers.push(providers[providerId]);
    }

    layers.push({
        layer: {
            onAdd: function () { },
            onRemove: function () { }
        },
        title: 'empty'
    })

    var ctrl = L.control.iconLayers(layers).addTo(mapObj);



};
///////////////////


function addMarkerCircleInMiniMap(lat, long, layerGroup)
{
    var myCircle = new L.CircleMarker(new L.LatLng(lat, long), { radius: 2 });
    layerGroup = layerGroup.addLayer(myCircle)
    return layerGroup;
}

function addMarker(coord, popupContent, popupOptionObj, markerObj) {
    if (!popupOptionObj) {
        popupOptionObj = {};
    }
    //var icon = L.divIcon({
    //    className: 'custom-div-icon',
    //    html: "<div style='background-color:#c30b82;' class='marker-pin'></div><i class='fa fa-camera awesome'></i>",
    //    iconSize: [30, 42],
    //    iconAnchor: [15, 42]
    //});
    const gasIcon = L.MakiMarkers.icon({
        icon: "beer",
        color: "#12a",
        size: "l"
    });

    marker = new L.marker(coord, { icon: gasIcon });

    console.log(marker);

    marker.options.highlight = "permanent";
    //marker.addTo(mapObj);
    mapObj.markers = marker;
    var popup = L.popup(popupOptionObj);
    popup.setContent(popupContent);

    // binding
    marker.bindPopup(popup);
    marker.addTo(markerGroup);


}

function onMapClick(e) {
    long = e.latlng.lng.toFixed(5);//lấy giá trị long
    lat = e.latlng.lat.toFixed(5);//lấy giá trị lat
    var html = `<h4>Toạ độ (` + lat + `,` + long + `)</h4>
        <button class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" onclick="showPopUp()"><i class="glyphicon glyphicon-plus"></i> Tạo phòng </button>`;
    popup
        .setLatLng(e.latlng)
        .setContent(html)
        .openOn(mapObj);//hiện popup
    console.log(e.latlng);
}
//
function screenFull(map) {
    L.control.fullscreen({
        position: 'topleft', // change the position of the button can be topleft, topright, bottomright or bottomleft, defaut topleft
        title: 'Show me the fullscreen !', // change the title of the button, default Full Screen
        titleCancel: 'Exit fullscreen mode', // change the title of the button when fullscreen is on, default Exit Full Screen
        content: null, // change the content of the button, can be HTML, default null
        forceSeparateButton: true, // force seperate button to detach from zoom buttons, default false
        forcePseudoFullscreen: true, // force use of pseudo full screen even if full screen API is available, default false
        fullscreenElement: false // Dom element to render in full screen, false by default, fallback to map._container
    }).addTo(map);
}


function getAllStoresInDb() {
    var popupContent = null;
    var popupOption = {
        className: "map-popup-content",
    };
    var name = null;


    $.ajax({
        type: 'POST',
        url: '/Store/getAllStores',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (1 == 1)
            {

                var layerGroup = new L.layerGroup([osm2]);
                var responsive = data.data;
                console.log(responsive.length);
                for (let i = 0; i < responsive.length; i++) {
                    if (responsive[i].StoreName == null) {
                        name = `Phòng trọ`;
                    } else {
                        name = responsive[i].StoreName;
                    }

                    popupContent = `<div class='left'><img src="/Images/92978289_651374272327767_5647810410020077568_n.png" width="100%" /></div>
        <div class='right'><h5>${name}</h5><br>
            <button class="btn btn-primary" data-toggle="modal" data-target="#exampleModal1" onclick="showPopUpDetailRoom(${responsive[i].MapID})"><i class="glyphicon glyphicon-eye-open"></i> Xem chi tiết </button>
</div>
            <div class='clearfix'></div>`;

                    addMarker([Number(responsive[i].Latitude), Number(responsive[i].Longtitude)], popupContent, popupOption);
                    layerGroup = addMarkerCircleInMiniMap(Number(responsive[i].Latitude), Number(responsive[i].Longtitude), layerGroup);
                }
                //var miniMap = new L.Control.MiniMap(layerGroup, { toggleDisplay: true }).addTo(mapObj);
                minimap.changeLayer(layerGroup);
            }
        }

    })
}


function showPopUp() {
    var str = `Tạo phòng ở toạ độ (${lat},${long})`;
    $('#exampleModalLabel').text(str);
}

function addRoom() {
    var url = `/Points/Main?lat=${lat}&lng=${long}`;

    var mapName = $('#mapName').val();//HẢI BÉO
    var phoneNumber = $('#phoneNumber').val();
    var address = $('#address').val();
    var moneyEachMonth = $('#moneyEachMonth').val();
    var acreageRoom = $('#acreageRoom').val();
    var descriptionDetail = $('#descriptionDetail').val();

    var parameters = `{MapName: '${mapName}',Latitude:'${lat}',Longitude:'${long}',
                          PhoneNumber:'${phoneNumber}',
                          Address:'${address}',
                          MoneyEachMonth:'${moneyEachMonth}',
                          AcreageRoom:'${acreageRoom}',
                          DescriptionDetail:'${descriptionDetail}'}`;//tạo string


    $.ajax({
        type: 'POST',
        url: '/Points/Save',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        data: parameters,
        success: function (data) {
            if (1 == 1) {
                alert("Tạo phòng trọ thành công");
                $(location).attr('href', url);//chuyển url
            }
        }

    })
}

function showPopUpDetailRoom(id) {

    $.ajax({
        type: 'POST',
        url: '/Points/getDetail?id=' + id,
        dataType: 'JSON',
        success: function (data) {
            //alert('Success');
            $('#mapName1').val(data[0].mapName);
            $('#phoneNumber1').val(data[0].phoneNumber);
            $('#address1').val(data[0].address);
            $('#moneyEachMonth1').val(data[0].moneyEachMonth);
            $('#acreageRoom1').val(data[0].acreageRoom);
            $('#descriptionDetail1').val(data[0].descriptionDetail);
        }
    })

    var str = `Chi tiết `;
    $('#exampleModalLabel1').text(str);
}

function getUrlParameters(parameter, staticURL, decode) {

    var currLocation = (staticURL.length) ? staticURL : window.location.search,
        parArr = currLocation.split("?")[1].split("&"),
        returnBool = true;

    for (var i = 0; i < parArr.length; i++) {
        parr = parArr[i].split("=");
        if (parr[0] == parameter) {
            return (decode) ? decodeURIComponent(parr[1]) : parr[1];
            returnBool = true;
        } else {
            returnBool = false;
        }
    }

    if (!returnBool) return false;
}


