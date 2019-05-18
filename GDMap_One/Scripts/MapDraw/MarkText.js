
function addMark(areas) {
    //添加文字
    //var sheshi = [{
    //    name: '开元湖',
    //    position: [112.45131, 34.61500],
    //    icon: 'https://a.amap.com/jsapi_demos/static/resource/img/停车场.png'
    //}, {
    //    name: '龙翔小区',
    //    position: [112.41111, 34.60725],
    //    icon: 'https://a.amap.com/jsapi_demos/static/resource/img/洗手间.png'
    //}, {
    //    name: '开元大道',
    //    position: [112.42111, 34.60925],
    //    icon: 'https://a.amap.com/jsapi_demos/static/resource/img/洗手间.png'
    //}]

    var facilities = [];
    var zoomStyleMapping1 = {
        14: 0,
        15: 0,
        16: 0,
        17: 0,
        18: 0,
        19: 0,
        20: 0
    };
    for (var i = 0; i < areas.length; i += 1) {
        var marker = new AMap.ElasticMarker({

            clickable: true,
            position: areas[i].position,
            zooms: [14, 20],
            styles: [{
                icon: {
                    img: areas[i].icon,
                    size: [0, 0],//可见区域的大小
                    ancher: [8, 16],//锚点
                    fitZoom: 14,//最合适的级别
                    scaleFactor: 2,//地图放大一级的缩放比例系数
                    maxScale: 1,//最大放大比例
                    minScale: 1//最小放大比例
                },
                label: {
                    content: areas[i].name,
                    offset: [-35, 0],
                    position: 'BM'
                }
            }],
            zoomStyleMapping: zoomStyleMapping1
        });
        if (areas[i].title !== "" && areas[i].intro !== "") {
            marker.title = areas[i].title;
            marker.intro = areas[i].intro;
            marker.on("click", markerClick);
        }
        facilities.push(marker);
    }
    map.add(facilities);
}
//var infoWindow = new AMap.InfoWindow({ offset: new AMap.Pixel(0, 0) });
//加载SimpleInfoWindow，loadUI的路径参数为模块名中 'ui/' 之后的部分

var infoWindow;
AMapUI.loadUI(['overlay/SimpleInfoWindow'], function (SimpleInfoWindow) {
     infoWindow = new SimpleInfoWindow({
        infoTitle: '<strong>这里是标题</strong>',
        infoBody: '<p>这里是内容。</p>'
    });
});
function markerClick(e) {
    infoWindow.open(map, e.target.getPosition());
    console.log(infoWindow.get$InfoTitle());
    infoWindow.setInfoTitle(e.target.title);
    infoWindow.setInfoBody(e.target.intro);

}