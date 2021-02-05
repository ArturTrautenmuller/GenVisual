var dragControl = { drag: false, targetId: 0 };

function getMousePos(canvas, evt) {
    var rect = canvas.getBoundingClientRect();
    return {
        x: evt.clientX - rect.left,
        y: evt.clientY - rect.top
    };
}

function chackTargetClick(mouse_x, mouse_y, target_x, target_y, radius) {
    var distance = Math.pow(Math.pow(mouse_x - target_x, 2) + Math.pow(mouse_y - target_y, 2), 0.5);
    if (distance <= radius) {
        return true;
    }
    else {
        return false;
    }
}

var canvasmap = document.getElementById("MapCanvas");

canvasmap.addEventListener("mousedown", function (evt) {
    var mousePos = getMousePos(canvasmap, evt);
    var targetsQtd = Object.keys(log["map"]["targets"]).length;
    var gen = parseInt(document.getElementById("selectGen").value);

    if (gen != -1) {
        dragControl = { drag: false, targetId: 0 };
        return;
    }

    for (var i = 1; i <= targetsQtd; i++) {
        
        var target_x = log["map"]["targets"][i]["coordenates"]["x"] * canvasWidth / mapMilesRangeX;
        var target_y = log["map"]["targets"][i]["coordenates"]["y"] * canvasHeight / mapMilesRangeY;
        if (chackTargetClick(mousePos.x, mousePos.y, target_x, target_y, circleRadius)) {
            dragControl.drag = true;
            dragControl.targetId = i;
        }        
       
    }


}, false);

canvasmap.addEventListener("mouseup", function (evt) {
    dragControl = { drag: false, targetId: 0 };


}, false);


canvasmap.addEventListener("mousemove", function (evt) {
    if (dragControl.drag) {
        var mousePos = getMousePos(canvasmap, evt);

        log["map"]["targets"][dragControl.targetId]["coordenates"]["x"] = (mousePos.x * mapMilesRangeX) / canvasWidth;
        log["map"]["targets"][dragControl.targetId]["coordenates"]["y"] = (mousePos.y * mapMilesRangeY) / canvasHeight;

        buildMap(-1);

    }


}, false);