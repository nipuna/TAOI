

var thumb = $('img#brandLogo');
var settings = {
    // Location of the server-side upload script
    action: 'upload.php',
    // File upload name
    name: 'userfile',
    // Additional data to send
    data: {},
    // Submit file as soon as it's selected
    autoSubmit: true,
    // The type of data that you're expecting back from the server.
    // html and xml are detected automatically.
    // Only useful when you are using json data as a response.
    // Set to "json" in that case. 
    responseType: false,
    // Class applied to button when mouse is hovered
    hoverClass: 'hover',
    // Class applied to button when AU is disabled
    disabledClass: 'disabled',
    // When user selects a file, useful with autoSubmit disabled
    // You can return false to cancel upload			
    onChange: function (file, extension) {
    },
    // Callback to fire before file is uploaded
    // You can return false to cancel upload
    onSubmit: function (file, extension) {
    },
    // Fired when file upload is completed
    // WARNING! DO NOT USE "FALSE" STRING AS A RESPONSE!
    onComplete: function (file, response) {
    }
};

function fillIamge(file, frm, actn, img, divImg) 
{

    settings.action = '/brands/imageDownloader'
    settings.name = 'logo1'

    submit();
   
    if (file.value != "") {

        var imgCnt = document.getElementById(divImg);
        if (imgCnt.style.visibility == "hidden") 
        {
            imgCnt.style.visibility = "visible";
            imgCnt.style.display = "block";
            document.getElementById('divNoImageSelected').style.visibility = "hidden";
            document.getElementById('divNoImageSelected').style.display = "none";
        }

        var fileName = fileFromPath(file.value);

        // sending request    
        var iframe = _createIframe();
        //var form = _createForm(iframe);
        var form = document.getElementById(frm);
        form.setAttribute('action', settings.action);
        form.setAttribute('target', iframe.name);

        // assuming following structure
        // div -> input type='file'
//        removeNode(this._input.parentNode);
//        removeClass(self._button, self._settings.hoverClass);

        var div = document.getElementById("divUploadButton");
        var newDiv = div.cloneNode(true);

        //form.appendChild(newDiv);

        form.submit();

        // request set, clean up                
        //removeNode(form); form = null;
        //removeNode(this._input); this._input = null;

        // Get response from iframe and fire onComplete event when ready
        this._getResponse(img, iframe, fileName);
        form.setAttribute('action', actn);
        form.setAttribute('target', '');

    }
}


function fileFromPath(file) {
    return file.replace(/.*(\/|\\)/, "");
}

/**
* Creates iframe with unique name
* @return {Element} iframe
*/
function _createIframe() 
{
    // We can't use getTime, because it sometimes return
    // same value in safari :(
    var id = getUID();

    // We can't use following code as the name attribute
    // won't be properly registered in IE6, and new window
    // on form submit will open
    // var iframe = document.createElement('iframe');
    // iframe.setAttribute('name', id);                        

    var iframe = toElement('<iframe src="javascript:false;" name="' + id + '" />');
    // src="javascript:false; was added
    // because it possibly removes ie6 prompt 
    // "This page contains both secure and nonsecure items"
    // Anyway, it doesn't do any harm.            
    iframe.setAttribute('id', id);

    iframe.style.display = 'none';
    document.body.appendChild(iframe);

    return iframe;
}

        
function _createForm(iframe) {

    // We can't use the following code in IE6
    // var form = document.createElement('form');
    // form.setAttribute('method', 'post');
    // form.setAttribute('enctype', 'multipart/form-data');
    // Because in this case file won't be attached to request                    
    var form = toElement('<form method="post" enctype="multipart/form-data"></form>');

    form.setAttribute('action', settings.action);
    form.setAttribute('target', iframe.name);
    form.style.display = 'none';
    document.body.appendChild(form);

    // Create hidden input element for each data key
    for (var prop in settings.data) {
        if (settings.data.hasOwnProperty(prop)) {
            var el = document.createElement("input");
            el.setAttribute('type', 'hidden');
            el.setAttribute('name', prop);
            el.setAttribute('value', settings.data[prop]);
            form.appendChild(el);
        }
    }
    return form;
}

/**
* Gets response from iframe and fires onComplete event when ready
* @param iframe
* @param file Filename to use in onComplete callback 
*/
function _getResponse(img, iframe, file) {
    // getting response
    var toDeleteFlag = false;

    addEvent(iframe, 'load', function () {

        if (// For Safari 
                    iframe.src == "javascript:'%3Chtml%3E%3C/html%3E';" ||
        // For FF, IE
                    iframe.src == "javascript:'<html></html>';") {
            // First time around, do not delete.
            // We reload to blank page, so that reloading main page
            // does not re-submit the post.

            if (toDeleteFlag) {
                // Fix busy state in FF3
                setTimeout(function () {
                    removeNode(iframe);
                }, 0);
            }

            return;
        }

        var doc = iframe.contentDocument ? iframe.contentDocument : window.frames[iframe.id].document;

        // fixing Opera 9.26,10.00
        if (doc.readyState && doc.readyState != 'complete') {
            // Opera fires load event multiple times
            // Even when the DOM is not ready yet
            // this fix should not affect other browsers
            return;
        }

        // fixing Opera 9.64
        if (doc.body && doc.body.innerHTML == "false") {
            // In Opera 9.64 event was fired second time
            // when body.innerHTML changed from false 
            // to server response approx. after 1 sec
            return;
        }

        var response;

        if (doc.XMLDocument) {
            // response is a xml document Internet Explorer property
            response = doc.XMLDocument;
        } else if (doc.body) {
            // response is html document or plain text
            response = doc.body.innerHTML;

            if (settings.responseType && settings.responseType.toLowerCase() == 'json') {
                // If the document was sent as 'application/javascript' or
                // 'text/javascript', then the browser wraps the text in a <pre>
                // tag and performs html encoding on the contents.  In this case,
                // we need to pull the original text content from the text node's
                // nodeValue property to retrieve the unmangled content.
                // Note that IE6 only understands text/html
                if (doc.body.firstChild && doc.body.firstChild.nodeName.toUpperCase() == 'PRE') {
                    response = doc.body.firstChild.firstChild.nodeValue;
                }

                if (response) {
                    response = eval("(" + response + ")");
                } else {
                    response = {};
                }
            }
        } else {
            // response is a xml document
            response = doc;
        }

        complete(img , response);

        // Reload blank page, so that reloading main page
        // does not re-submit the post. Also, remember to
        // delete the frame
        toDeleteFlag = true;

        // Fix IE mixed content issue
        iframe.src = "javascript:'<html></html>';";
    });
}

function getUID() {
    var id = 0;
    return 'ValumsAjaxUpload' + id++;
}



function toElement(html) {
    var div = document.createElement('div');
    div.innerHTML = html;
    var el = div.firstChild;
    return div.removeChild(el);
}



/**
* Attaches event to a dom element.
* @param {Element} el
* @param type event name
* @param fn callback This refers to the passed element
*/
function addEvent(el, type, fn) {
    if (el.addEventListener) {
        el.addEventListener(type, fn, false);
    } else if (el.attachEvent) {
        el.attachEvent('on' + type, function () {
            fn.call(el);
        });
    } else {
        throw new Error('not supported or DOM not loaded');
    }
}


function removeClass(el, name) {
    var re = new RegExp('\\b' + name + '\\b');
    el.className = el.className.replace(re, '');
}

function removeNode(el) {
    el.parentNode.removeChild(el);
}

function submit() {
    $('div.preview').addClass('loading');
}

function complete(img, response) {
   
//    $('div.preview').removeClass('loading');
//    document.getElementById(img).src = response;
    //thumb.attr('src', response);
}
