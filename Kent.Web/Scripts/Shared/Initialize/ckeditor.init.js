CKEDITOR.dtd.$removeEmpty['span'] = false;
CKEDITOR.dtd.$removeEmpty['i'] = false;

function buildCkEditor(selectorId, options) {
    var defaultOptions = {
        extraPlugins: 'widget,webedstylesheetparser,webedcurlybracket,fontawesome,lineutils,html5validation,youtube,internallink',
    };
    $.extend(options, defaultOptions);

    var editor = CKEDITOR.replace(selectorId, options);

    $('[type=submit]').bind('click', function () {
        editor.updateElement();
    });

    return editor;
};