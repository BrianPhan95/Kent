/**
 * Copyright (c) 2014, CKSource - Frederico Knabben. All rights reserved.
 * Licensed under the terms of the MIT License (see LICENSE.md).
 *
 * The webedcurlybracket plugin dialog window definition.
 *
 * Created out of the CKEditor Plugin SDK:
 * http://docs.ckeditor.com/#!/guide/plugin_sdk_sample_1
 */

// Our dialog definition.
CKEDITOR.dialog.add('webedcurlybracketDialog', function (editor) {
    return {

        // Basic properties of the dialog window: title, minimum size.
        title: 'Curly Bracket',
        minWidth: 400,
        minHeight: 100,

        // Dialog window content definition.
        contents: [
			{
			    // Definition of the Basic Settings dialog tab (page).
			    id: 'tab-setup',
			    label: 'Settings',

			    // The tab content.
			    elements: [
					{
					    // Text input field for the curlybracketeviation text.
					    type: 'text',
					    id: 'curlybracket',
					    label: 'Curly bracket',

					    // Validation checking whether the field is not empty.
					    validate: CKEDITOR.dialog.validate.notEmpty("Curly bracket field cannot be empty."),

					    // Called by the main setupContent method call on dialog initialization.
					    setup: function (element) {
					        console.log(element);
					        console.log(element.getText());
					        this.setValue(element.getText());
					    },

					    // Called by the main commitContent method call on dialog confirmation.
					    commit: function (element) {
					        element.setText(this.getValue());
					    }
					},
				    {
				        type: 'button',
				        id: 'buttonId',
				        label: 'Configure',
				        title: 'Setup curly bracket',
				        style: 'float:right',
				        onLoad: function () {

				        },
				        onClick: function () {
				            var curlyBracket = this.getDialog().getContentElement('tab-setup', 'curlybracket').getValue();
				            var href;
				            if (curlyBracket == '') {
				                href = "/Admin/CurlyBrackets/SelectCurlyBrackets?isCkEditor=true";
				            } else {
				                href = "/Admin/CurlyBrackets/GenerateCurlyBracket?id=" + curlyBracket + "&isCkEditor=true";
				            }
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

        // Invoked when the dialog is loaded.
        onShow: function () {

            // Get the selection from the editor.
            var selection = editor.getSelection();

            // Get the element at the start of the selection.
            var element = selection.getStartElement();

            // Get the <curlybracket> element closest to the selection, if it exists.
            if (element)
                element = element.getAscendant('curlybracket', true);

            // Create a new <curlybracket> element if it does not exist.
            if (!element || element.getName() != 'curlybracket') {
                element = editor.document.createElement('curlybracket');

                $(element).attr("contentEditable", false);

                // Flag the insertion mode for later use.
                this.insertMode = true;
            }
            else
                this.insertMode = false;

            // Store the reference to the <curlybracket> element in an internal property, for later use.
            this.element = element;

            // Invoke the setup methods of all dialog window elements, so they can load the element attributes.
            if (!this.insertMode)
                this.setupContent(this.element);
        },

        // This method is invoked once a user clicks the OK button, confirming the dialog.
        onOk: function () {

            // The context of this function is the dialog object itself.
            // http://docs.ckeditor.com/#!/api/CKEDITOR.dialog
            var dialog = this;

            // Create a new <curlybracket> element.
            var curlybracket = this.element;

            // Invoke the commit methods of all dialog window elements, so the <curlybracket> element gets modified.
            this.commitContent(curlybracket);

            // Finally, if in insert mode, insert the element into the editor at the caret position.
            if (this.insertMode)
                editor.insertElement(curlybracket);
        }
    };
});
