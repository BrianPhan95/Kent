/**
 * Copyright (c) 2014, CKSource - Frederico Knabben. All rights reserved.
 * Licensed under the terms of the MIT License (see LICENSE.md).
 *
 * Basic sample plugin inserting webedcurlybracketeviation elements into the CKEditor editing area.
 *
 * Created out of the CKEditor Plugin SDK:
 * http://docs.ckeditor.com/#!/guide/plugin_sdk_sample_1
 */

// Register the plugin within the editor.
CKEDITOR.plugins.add( 'webedcurlybracket', {

	// Register the icons.
	icons: 'webedcurlybracket',

	// The plugin initialization logic goes inside this method.
	init: function( editor ) {

		// Define an editor command that opens our dialog window.
		editor.addCommand( 'webedcurlybracket', new CKEDITOR.dialogCommand( 'webedcurlybracketDialog' ) );

		// Create a toolbar button that executes the above command.
		editor.ui.addButton( 'webedcurlybracket', {

			// The text part of the button (if available) and the tooltip.
		    label: 'Curly Bracket',

			// The command to execute on click.
			command: 'webedcurlybracket',

			// The button placement in the toolbar (toolbar group name).
			toolbar: 'insert'
		});
	    
		editor.on('doubleclick', function (evt) {
		    var element = evt.data.element;

		    if (element.is('curlybracket')) {
		        editor.execCommand('webedcurlybracket');
		        //evt.data.dialog = 'webedcurlybracketDialog';
		    }
		});


		if ( editor.contextMenu ) {
			
			// Add a context menu group with the Edit webedcurlybracketeviation item.
			editor.addMenuGroup( 'webedcurlybracketGroup' );
			//editor.addMenuItem( 'webedcurlybracketItem', {
			//	label: 'Edit curly bracket',
			//	icon: this.path + 'icons/webedcurlybracket.png',
			//	command: 'webedcurlybracket',
			//	group: 'webedcurlybracketGroup'
			//});

			//editor.contextMenu.addListener( function( element ) {
			//	if ( element.getAscendant( 'curlybracket', true ) ) {
			//		return { webedcurlybracketItem: CKEDITOR.TRISTATE_OFF };
			//	}
			//});
		}

		// Register our dialog file -- this.path is the plugin folder path.
		CKEDITOR.dialog.add( 'webedcurlybracketDialog', this.path + 'dialogs/webedcurlybracket.js' );
	}
});
