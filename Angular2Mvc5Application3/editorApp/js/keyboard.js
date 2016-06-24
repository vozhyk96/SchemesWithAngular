$(document).ready(function () {

    var copiedObject;
    var copiedObjects = new Array();
    var canvas = document.getElementById("canvas").fabric;

    createListenersKeyboard();

    function createListenersKeyboard() {
        document.onkeydown = onKeyDownHandler;
        //document.onkeyup = onKeyUpHandler;
    }

    function onKeyDownHandler(event) {
        //event.preventDefault();

        var key;
        if (window.event) {
            key = window.event.keyCode;
        }
        else {
            key = event.keyCode;
        }

        switch (key) {
            // Copy (Ctrl+C)
            case 67: // Ctrl+C
                if (event.ctrlKey) {
                    event.preventDefault();
                    copy();
                }
                break;
                // Paste (Ctrl+V)
            case 86: // Ctrl+V
                if (event.ctrlKey) {
                    event.preventDefault();
                    paste();
                }
                break;
            case 46: // Delete
                event.preventDefault();
                deleteElements();
                break;
            default:
                // TODO
                break;
        }
    }


    // TODO gupy kopiują się do punktu 0,0 -  nie powinno tak być
    function copy() {
        if (canvas.getActiveGroup()) {
            for (var i in canvas.getActiveGroup().objects) {
                var object = fabric.util.object.clone(canvas.getActiveGroup().objects[i]);
                object.set("top", object.top + 20);
                object.set("left", object.left + 20);
                copiedObjects[i] = object;
            }
        }
        else if (canvas.getActiveObject()) {
            var object = fabric.util.object.clone(canvas.getActiveObject());
            object.set("top", object.top + 5);
            object.set("left", object.left + 5);
            copiedObject = object;
            copiedObjects = new Array();
        }
    }

    function paste() {
        if (copiedObjects.length > 0) {
            for (var i in copiedObjects) {
                canvas.add(copiedObjects[i]);
            }
        }
        else if (copiedObject) {
            canvas.add(copiedObject);
        }
        canvas.renderAll();
    }

    function deleteElements() {
        if (canvas.getActiveGroup()) {
            canvas.getActiveGroup().forEachObject(function (o) {
                canvas.remove(o)
            });
            canvas.discardActiveGroup().renderAll();
        } else {
            canvas.remove(canvas.getActiveObject());
        }
    }

    function download() {
        var dataUrl = canvas.toDataURL('image/jpeg');
        this.href = dataUrl;
        console.log(dataUrl);
        var Data = new FormData();
        Data.append("picture", dataUrl)
        Data.append("url",document.URL)
        this.href = dataUrl;
        $.ajax({
            type: "POST",
            url: "/Post/EditorApp",
            processData: false,
            contentType: false,
            data: Data,
            success: function () {
                window.location.href = document.referrer;
                $("#txtImg").val(response);
                $("#imgPreview").attr('src', '/Upload/' + response);
               
            },
            error: function () {
            alert('er');
        }
            
        });
    };
    downloadLnk.addEventListener('click', download, false);

    /* delete elements with menu button */
    $('#delete-item').click(deleteElements);
});
