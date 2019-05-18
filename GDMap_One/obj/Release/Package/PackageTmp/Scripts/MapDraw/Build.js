function addBuild(options) {
    var buildingLayer = new AMap.Buildings({ zIndex: 130, merge: false, sort: false, zooms: [17, 20] });
    options.areas.forEach(e => {
        new AMap.Polygon({
            bubble: true,
            fillColor: '#ff9900',
            fillOpacity: 0.2,
            strokeWeight: 1,
            path: e.path,
            map: map
        });
    });
    buildingLayer.setStyle(options);
    map.setLayers([new AMap.TileLayer({}),buildingLayer]);
} 
