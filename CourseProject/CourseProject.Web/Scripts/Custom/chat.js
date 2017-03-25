$(document).ready(function () {

    var chatHub = $.connection.chatHub;
    $.connection.hub.start().done();

    chatHub.client.chatWith = chatWith;
    chatHub.client.showUsernameError = showUsernameError;
    chatHub.client.addChatMessage = addChatMessage;

    $('#submit-user').click(function (ev) {
        var previousUsername = $('#chat-panel').attr("data-username");
        if (previousUsername) {
            chatHub.server.disconnect(previousUsername);
        }

        var username = $('#username').val();
        chatHub.server.checkUsernameAndConnect(username);
    });

    $('#msg-btn').click(function (ev) {
        var message = $('#msg-input').val();
        var username = $('#chat-panel').attr('data-username');
        chatHub.server.sendMessage(username, message);
        var encodedMessage = $('<div>').text(message).html();

        var element = '<li class="right clearfix">' +
         '<span class="chat-img pull-right"><img src="http://placehold.it/50/FA6F57/fff&text=ME" alt="User Avatar" class="img-circle" /></span>' +
            '<div class="chat-body clearfix">' +
                '<div class="header"><strong class="pull-right primary-font">Me</strong></div>' +
                `<p>${encodedMessage}</p>`
        '</div>'
        '</li>';

        $('#messages').append(element);
    })

});

function chatWith(username) {
    $('#error-msg').text('');

    const chatPanel = $('#chat-panel');
    chatPanel.removeClass('display-none');
    chatPanel.attr('data-username', username);

    $("#messages").html("");
}

function showUsernameError() {
    const errorMsg = $('#error-msg');
    errorMsg.text('There is no user with this username.');
}

function addChatMessage(username, message) {
    const element = '<li class="left clearfix">' +
            '<span class="chat-img pull-left"><img src="http://placehold.it/50/55C1E7/fff&text=U" alt="User Avatar" class="img-circle" /></span>' +
            '<div class="chat-body clearfix">' +
                `<div class="header"><strong class="primary-font">${username}</strong></div>` +
                `<p>${message}</p>` +
            '</div>' +
        '</li>';

    $('#messages').append(element);
}