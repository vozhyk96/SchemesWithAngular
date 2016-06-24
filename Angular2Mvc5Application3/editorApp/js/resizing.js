/*
 * sprawia że ramki obiektów się nie rozciągają tylko pozostają grubości zadanej
 */

$( document ).ready(function() {
	var canvas = document.getElementById("canvas").fabric;
	
// set up a listener for the event where the object has been modified
	canvas.observe('object:modified', function (e) {
	    e.target.resizeToScale();
	});
	
	// customise fabric.Object with a method to resize rather than just scale after tranformation
	fabric.Object.prototype.resizeToScale = function () {
	    // resizes an object that has been scaled (e.g. by manipulating the handles), setting scale to 1 and recalculating bounding box where necessary
	    switch (this.type) {
	        case "circle":
	            this.radius *= this.scaleX;
	            this.scaleX = 1;
	            this.scaleY = 1;
	            break;
	        case "ellipse":
	            this.rx *= this.scaleX;
	            this.ry *= this.scaleY;
	            this.width = this.rx * 2;
	            this.height = this.ry * 2;
	            this.scaleX = 1;
	            this.scaleY = 1;
	            break;
	        case "polygon":
	        case "polyline":
	            var points = this.get('points');
	            for (var i = 0; i < points.length; i++) {
	                var p = points[i];
	                p.x *= this.scaleX
	                p.y *= this.scaleY;
	            }
	            this.scaleX = 1;
	            this.scaleY = 1;
	            this.width = this.getBoundingBox().width;
	            this.height = this.getBoundingBox().height;
	            break;
	        case "triangle":
	        case "line":
	        case "rect":
	            this.width *= this.scaleX;
	            this.height *= this.scaleY;
	            this.scaleX = 1;
	            this.scaleY = 1;
	        default:
	            break;
	    }
	}

	
	canvas.on('object:moving', function(e) {
	    var p = e.target;
	    p.line && p.line.set({ 'x2': p.left, 'y2': p.top });
	    canvas.renderAll();
	  
	  });
	
});