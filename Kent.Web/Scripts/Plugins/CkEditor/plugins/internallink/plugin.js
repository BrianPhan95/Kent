CKEDITOR.plugins.add('internallink', {
    requires: 'widget',
    icons: 'internallink',
    init: function (editor) {

        // Plugin logic goes here...
        editor.addCommand( 'internallink', new CKEDITOR.dialogCommand( 'internallinkDialog' ) );

        editor.ui.addButton('InternalLink', {
            label: 'Add Internal Link',
            command: 'internallink',
            toolbar: 'link',
            icon: this.path + 'icons/internallink.png'
        });
        editor.on('doubleclick', function (evt) {
            var element = evt.data.element;
            if (element.getAttribute('internallink') === 'internallink') {
                evt.data.dialog = 'internallinkDialog';
            }
        });
        
        CKEDITOR.dialog.add('internallinkDialog', this.path + 'dialogs/internallink.js');
    }
});
