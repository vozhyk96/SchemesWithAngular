/*
 * Dodawanie elementów do canvasa i definicje elementów
 */

$(document).ready(function () {
    var canvas = document.getElementById("canvas").fabric;
    var step = 10;
    fabric.Object.prototype.originX = fabric.Object.prototype.originY = 'center';



    /*------- OBJECTS -----------*/

    function newRectangle(x, y) {
        var newX = Math.round(x / step) * step;
        var newTop = Math.round(y / step) * step - 50;

        function drawLine(coords) {
            return new fabric.Line(coords, {
                stroke: 'black',
                strokeWidth: 4,
                selectable: true,
                hasControls: false,
                hasBorders: false
            });
        }


        var group = new fabric.Group([
            //makeRectangle(),
            drawLine([newX - 80, newTop, newX - 40, newTop]), //left connector
            drawLine([newX - 40, newTop - 20, newX - 40, newTop + 20]),  //left vertical
            drawLine([newX + 40, newTop - 20, newX + 40, newTop + 20]), //right vertical
            drawLine([newX - 40, newTop - 20, newX + 40, newTop - 20]), // top horiz
            drawLine([newX - 40, newTop + 20, newX + 40, newTop + 20]), //bottom horiz
            drawLine([newX + 40, newTop, newX + 80, newTop]) //right connector
        ], {});
        group.setControlsVisibility({
            mt: false, // middle top disable
            mb: false, // midle bottom
            ml: false, // middle left
            mr: false, // I think you get it
            br: false, // I think you get it
            bl: false, // I think you get it
            tl: false, // I think you get it
            tr: false // I think you get it
        });
        canvas.add(group);
    }
    function newText(x, y) {
        var newX = Math.round(x / step) * step;
        var newTop = Math.round(y / step) * step - 50;
        var text = new fabric.IText('  ', {
            fontFamily: 'arial',
            fontSize: 24,
            left: newX,
            top: newTop,
            hasControls: false,
            hasBorders: true,
            selectable: true
        });

        canvas.add(text);
    }


    function newCircle(x, y) {
        var newX = Math.round(x / step) * step;
        var newTop = Math.round(y / step) * step - 50;

        function drawLine(coords) {
            return new fabric.Line(coords, {
                stroke: 'black',
                strokeWidth: 4,
                selectable: true,
                hasControls: false,
                hasBorders: false
            });
        }

        var group = new fabric.Group([
            drawLine([newX - 80, newTop, newX - 10, newTop]), //left connector
            drawLine([newX - 10, newTop - 30, newX - 10, newTop + 30]),  //left vertical
            drawLine([newX + 10, newTop - 30, newX + 10, newTop + 30]), //right vertical
            drawLine([newX + 10, newTop, newX + 80, newTop]) //right connector
        ], {});

        group.setControlsVisibility({
            mt: false, // middle top disable
            mb: false, // midle bottom
            ml: false, // middle left
            mr: false, // I think you get it
            br: false, // I think you get it
            bl: false, // I think you get it
            tl: false, // I think you get it
            tr: false // I think you get it
        });


        canvas.add(group);
    }

    function newEllipse(x, y) {
        var newX = Math.round(x / step) * step;
        var newTop = Math.round(y / step) * step - 50;

        function drawLine(coords) {
            return new fabric.Line(coords, {
                stroke: 'black',
                strokeWidth: 4,
                selectable: true,
                hasControls: false,
                hasBorders: false
            });
        }
        function drawCircle() {
            return new fabric.Circle({
                left: newX,
                top: newTop,
                fill: "rgba(0, 0, 0, 0)",
                strokeWidth: 4,
                stroke: 'black',
                radius: 40
            });
        }

        var group = new fabric.Group([
            drawCircle(), //left connector
            drawLine([newX, newTop - 30, newX, newTop + 30]),  // vertical
            drawLine([newX, newTop, newX + 30, newTop - 30]), //emitter
            drawLine([newX, newTop, newX + 30, newTop + 30]), //collector
            drawLine([newX + 30, newTop - 28, newX + 30, newTop - 60]), //emitter connect
            drawLine([newX + 30, newTop + 28, newX + 30, newTop + 60]), //collector connect
            drawLine([newX - 60, newTop, newX, newTop]) //base connector
        ], {});
        group.setControlsVisibility({
            mt: false, // middle top disable
            mb: false, // midle bottom
            ml: false, // middle left
            mr: false, // I think you get it
            br: false, // I think you get it
            bl: false, // I think you get it
            tl: false, // I think you get it
            tr: false // I think you get it
        });

        canvas.add(group);
    }

    function newLine(x, y) {
        var newX = Math.round(x / step) * step;
        var newTop = Math.round(y / step) * step - 50;

        function drawLine(coords) {
            return new fabric.Line(coords, {
                stroke: 'black',
                strokeWidth: 4,
                selectable: true,
                hasControls: false,
                hasBorders: false
            });
        }
        function drawCircle1() {
            return new fabric.Circle({
                radius: 20,
                left: newX - 20,
                top: newTop - 20,
                angle: 45,
                startAngle: 0,
                endAngle: Math.PI,
                fill: "rgba(0, 0, 0, 0)",
                strokeWidth: 4,
                stroke: 'black',
            });
        }
        function drawCircle2() {
            return new fabric.Circle({
                radius: 20,
                left: newX - 10,
                top: newTop - 20,
                angle: 45,
                startAngle: 0,
                endAngle: Math.PI,
                fill: "rgba(0, 0, 0, 0)",
                strokeWidth: 4,
                stroke: 'black',
            });
        }
        function drawCircle3() {
            return new fabric.Circle({
                radius: 20,
                left: newX + 10,
                top: newTop - 20,
                angle: 45,
                startAngle: 0,
                endAngle: Math.PI,
                fill: "rgba(0, 0, 0, 0)",
                strokeWidth: 4,
                stroke: 'black',
            });
        }
        function drawCircle4() {
            return new fabric.Circle({
                radius: 20,
                left: newX + 20,
                top: newTop - 20,
                angle: 45,
                startAngle: 0,
                endAngle: Math.PI,
                fill: "rgba(0, 0, 0, 0)",
                strokeWidth: 4,
                stroke: 'black',
            });
        }
        function drawCircle5() {
            return new fabric.Circle({
                radius: 20,
                left: newX,
                top: newTop - 20,
                angle: 45,
                startAngle: 0,
                endAngle: Math.PI,
                fill: "rgba(0, 0, 0, 0)",
                strokeWidth: 4,
                stroke: 'black',
            });
        }



        var group = new fabric.Group([
            //makeRectangle(),
            drawLine([newX - 80, newTop, newX, newTop]), //left connector
            drawCircle1(),
            drawCircle2(),
            drawCircle3(),
            drawCircle4(),
            drawCircle5(),
            drawLine([newX, newTop, newX + 80, newTop]) //right connector
        ], {});
        group.setControlsVisibility({
            mt: false, // middle top disable
            mb: false, // midle bottom
            ml: false, // middle left
            mr: false, // I think you get it
            br: false, // I think you get it
            bl: false, // I think you get it
            tl: false, // I think you get it
            tr: false // I think you get it
        });
        canvas.add(group);


    }


    function newArrow(x, y) {

        var newX = Math.round(x / step) * step;
        var newTop = Math.round(y / step) * step - 50;

        function drawLine(coords) {
            return new fabric.Line(coords, {
                stroke: 'black',
                strokeWidth: 4,
                selectable: true,
                hasControls: false,
                hasBorders: false
            });
        }

        var group = new fabric.Group([
            //makeRectangle(),
            drawLine([newX, newTop, newX + 100, newTop]), //

        ], {});
        group.setControlsVisibility({
            mt: false, // middle top disable
            mb: false, // midle bottom
            ml: false, // middle left
            mr: false, // I think you get it
            br: false, // I think you get it
            bl: false, // I think you get it
            tl: false, // I think you get it
            tr: false // I think you get it
        });
        canvas.add(group);
    }



    function createLine(x, y) {
        var points = [x, y, x + 1000, y];
        return new fabric.Line(points, {
            stroke: 'black',
            strokeWidth: 4,
            selectable: true
        });
    }


    function makeLine(coords) {
        return new fabric.Line(coords, {
            stroke: 'black',
            strokeWidth: 4,
            selectable: false
        });
    }

    /* ---- dodanie obiektu po przycisku----*/
    var addRectangle = document.getElementById('add-rectangle');
    addRectangle.onclick = function () {
        canvas.add(createRectangleInTheMiddle());
    }

    /* ---- dodanie obiektu Onclick----*/
    canvas.on('mouse:down', function (options) {
        if (options.target)
            return;
        var elem = document.getElementsByClassName("activePen");
        for (var i = 0; i < elem.length; i++) {
            switch (elem[i].id) {
                case "Hsquare":
                    newRectangle(options.e.clientX, options.e.clientY);
                    break;
                case "Hcircle":
                    newCircle(options.e.clientX, options.e.clientY);
                    break;
                case "Hellipse":
                    newEllipse(options.e.clientX, options.e.clientY);
                    break;
                case "Hline":
                    newLine(options.e.clientX, options.e.clientY);
                    break;
                case "Harrow":
                    newArrow(options.e.clientX, options.e.clientY);
                    break;
                case "Htext":
                    newText(options.e.clientX, options.e.clientY);
                    break;
                default:
                    ;// do nothing
            }
        }

    });
    canvas.on('object:rotating', function (e) {
        var p = e.target;
        var angle = Math.round(p.angle / 90) * 90;
        p.angle = angle;

        canvas.renderAll();
    });

    canvas.on('object:moving', function (e) {
        var p = e.target;

        var left = Math.round(p.left / step) * step;
        var top = Math.round(p.top / step) * step;

        //p.line1 && p.line1.set({ 'x2': left, 'y2': top });
        //p.line2 && p.line2.set({ 'x1': left, 'y1': top });

        p.setTop(top);
        p.setLeft(left);
        canvas.renderAll();
    });

    //canvas.on('object:moving', function(e) {
    //    var p = e.target;
    //    p.text && p.text.set({ 'left': p.left, 'top': p.top });
    //    canvas.renderAll();
    //  });

    //handle moving object
    canvas.on('object:moving', function (event) {
        //var obj = event.target;
        //intersectingCheck(obj);
    });

    function intersectingCheck(activeObject) {
        activeObject.setCoords();
        if (typeof activeObject.refreshLast != 'boolean') {
            activeObject.refreshLast = true
        }
        ;

        //loop canvas objects
        activeObject.canvas.forEachObject(function (targ) {
            if (targ === activeObject) return; //bypass self

            //check intersections with every object in canvas
            if (activeObject.intersectsWithObject(targ)
                || activeObject.isContainedWithinObject(targ)
                || targ.isContainedWithinObject(activeObject)) {
                //objects are intersecting - deny saving last non-intersection position and break loop
                if (typeof activeObject.lastLeft == 'number') {
                    activeObject.left = activeObject.lastLeft;
                    activeObject.top = activeObject.lastTop;
                    activeObject.text && activeObject.text.set({
                        'left': activeObject.lastLeft,
                        'top': activeObject.lastTop
                    });
                    activeObject.refreshLast = false;
                    return;
                }
            }
            else {
                activeObject.refreshLast = true;
            }
        });

        if (activeObject.refreshLast) {
            //save last non-intersecting position if possible
            activeObject.lastLeft = activeObject.left
            activeObject.lastTop = activeObject.top;
        }
    }

});