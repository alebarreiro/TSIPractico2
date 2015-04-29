//WebSocket implementation

var WebSocket = WebSocket || MozWebSocket;
var myWS = new WebSocket("ws://mvcemployees.azurewebsites.net/");

myWS.onopen = function (evt) {
    myWS.send(user);
    //"@HttpContext.Current.User.Identity.Name"
};

myWS.onmessage = function (evt) {
    obj = JSON.parse(evt.data.toString());
    $("#chat_div").chatbox("option", "boxManager").addMsg(obj.id, obj.msg);
};

myWS.onclose = function (evt) {
    //alert("close socket!");
};

function SendMessage() {
    var text = document.getElementById("chatbox").value
    myWS.send(text);
};

//uichat Implementation
var box = null;
$(document).ready(function () {
    if (box) {
        box.chatbox("option", "boxManager").toggleBox();
    }
    else {
        box = $("#chat_div").chatbox({
            id: "chat_div",
            user: { key: "value" },
            title: "chat",
            messageSent: function (id, user, msg) {
                myWS.send(msg);
            }
        });
        //minimizo
        box.chatbox("toggleContent");
    }
});