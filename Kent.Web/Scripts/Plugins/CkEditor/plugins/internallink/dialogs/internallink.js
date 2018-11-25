CKEDITOR.dialog.add('internallinkDialog', function (editor) {
    return {
        // ... The dialog definition comes here ...
        title: 'Internal Link',
        minWidth: 400,
        minHeight: 100,

        contents: [
           {
               id: 'tab-setup',
               label: 'Settings',
               elements: [
                   // UI elements of the first tab will be defined here.
                   {
                       type: 'text',
                       id: 'Title',
                       label: 'Title',
                       validate: CKEDITOR.dialog.validate.notEmpty("Title field cannot be empty."),
                       setup: function (element) {
                           this.setValue(element.getText());
                       },
                       commit: function (element) {
                           element.setText(this.getValue());
                       }
                   },
                   {
                       type: 'text',
                       id: 'URL',
                       label: 'URL',
                       validate: CKEDITOR.dialog.validate.notEmpty("URL field cannot be empty."),
                       setup: function (element) {
                           this.setValue(element.getAttribute("href"));
                       },
                       commit: function (element) {
                           element.setAttribute('href', this.getValue());
                       }
                   },
                   {
                       type: 'button',
                       id: 'buttonId',
                       label: 'Site Map',
                       title: 'Browser link on site map',
                       style: 'float:right',
                       onLoad: function () {

                       },
                       onClick: function () {
                           
                           //var url = this.getDialog().getContentElement('tab-setup', 'URL').getValue();
                           var href = "/Admin/SiteMap/GenerateInternalLink";
                           //if (url == '') {
                           //    href = "/Admin/SiteMap/GenerateInternalLink?isCkEditor=true";
                           //} else {
                           //    href = "/Admin/SiteMap/GenerateInternalLink?url=" + curlyBracket + "&isCkEditor=true";
                           //}
                           siteHelper.showPopup({
                               href: href,
                               type: "iframe",
                               width: "1000px",
                               height: "650px"
                           });
                       }
                   }
               ]
           }
        ],
        
        onShow: function () {
            var selection = editor.getSelection();
            var element = selection.getStartElement();

            if (element)
                element = element.getAscendant('a', true);

            if (!element || element.getName() != 'a') {
                element = editor.document.createElement('a');
                this.insertMode = true;
            }
            else
                this.insertMode = false;
            element.setAttribute('internallink', 'internallink');
            this.element = element;
            if (!this.insertMode)
                this.setupContent(this.element);
        },
        
        onOk: function () {
            var dialog = this;
            var abbr = this.element;
            this.commitContent(abbr);

            if (this.insertMode)
                editor.insertElement(abbr);
        }

    };
});
