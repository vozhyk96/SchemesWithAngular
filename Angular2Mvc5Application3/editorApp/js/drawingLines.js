///*
// * Rysowanie lini przeciągających się po kliknięciu
// * 
// */
//
//$( document ).ready(function() {
//var canvas = document.getElementById("canvas").fabric;
//
//var line, isDown;
//
//canvas.on('mouse:down', function(o){
//	 var elem = document.getElementsByClassName("activePen");
//	   for (var i = 0; i < elem.length; i++) {
//	 	if(elem[i].id=="Hline") {
//			  isDown = true;
//			  var pointer = canvas.getPointer(o.e);
//			  var points = [ pointer.x, pointer.y, pointer.x, pointer.y ];
//			  line = new fabric.Line(points, {
//				stroke: 'black',
//				strokeWidth: 8,                    
//			    originX: 'center',
//			    originY: 'center',
//			    selectable: true
//			  });
//			  canvas.add(line);
//		}}
//});
//
//canvas.on('mouse:move', function(o){
//  if (!isDown) return;
//  var pointer = canvas.getPointer(o.e);
//  line.set({ x2: pointer.x, y2: pointer.y });
//  canvas.renderAll();
//});
//
//canvas.on('mouse:up', function(o){
//  isDown = false;
//});
//
//});