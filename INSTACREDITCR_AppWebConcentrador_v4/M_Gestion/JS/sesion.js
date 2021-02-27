//https://javascript-compressor.com/
//para comprimir este archivo
(function (global, undefined) {
    var notificationTimeout = 1740;var sessionTimeout = 60;var second = 1000;var seconds = sessionTimeout;var secondsBeforeShow = notificationTimeout;var mainLabel;var notification;var demo = {};var timers = {};
    function pageLoad(sender){
        mainLabel = $get("mainLbl");
        notification = $find(demo.notificationID);
        var xmlPanel = notification._xmlPanel;
        xmlPanel.set_enableClientScriptEvaluation(true);
        resetTimer("mainLblCounter", updateMainLabel);
    };
    function notification_showing(sender, args) {
        mainLabel.innerHTML = 0;
        resetTimer("timeLeftCounter", updateTimeLabel);
        stopTimer("mainLblCounter");
    }
    function notification_hidden() {
        updateMainLabel(true);
        mainLabel.style.display = "";
        resetTimer("mainLblCounter", updateMainLabel);
    }

    function updateMainLabel(toReset) {
        secondsBeforeShow = toReset ? notificationTimeout : secondsBeforeShow - 1;
        mainLabel.innerHTML = secondsBeforeShow;
    }
    function updateTimeLabel() {
        var sessionExpired = (seconds === 0);
        if (sessionExpired) {
            stopTimer("timeLeftCounter");
            expireSession();
        }
        else {
            var timeLbl = $get("timeLbl");
            timeLbl.innerHTML = seconds--;
        }
    }
    function ContinueSession() {
        notification.update();
        notification.hide();
        var showIntervalStorage = notification.get_showInterval();
        notification.set_showInterval(0);
        notification.set_showInterval(showIntervalStorage);
        stopTimer("timeLeftCounter");
        seconds = sessionTimeout;
        updateMainLabel(true);
    }
    function movement() {        
        stopTimer("timeLeftCounter");
        seconds = sessionTimeout;
        updateMainLabel(true);
    }
    function expireSession() {
        global.location.href = notification.get_value();
    }
    function stopTimer(timer) {
        global.clearInterval(timers[timer]);
        delete timers[timer];
    };
    function resetTimer(timer, func) {
        var delegate = Function.createDelegate(this, func);
        stopTimer(timer);
        timers[timer] = global.setInterval(delegate, second);
    };
    function serverID(name, id) {
        demo[name] = id;
    }
    function serverIDs(obj) {
        for (var name in obj) {
            serverID(name, obj[name]);
        }
    }
    global.serverIDs = serverIDs;
    global.ContinueSession = ContinueSession;
    global.notification_hidden = notification_hidden;
    global.notification_showing = notification_showing;
    global.movement = movement;
    Sys.Application.add_load(pageLoad);
})(window);