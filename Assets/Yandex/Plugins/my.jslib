mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },
GiveMePlayerData: function () {
  myGameInstance.SendMessage('Yandex', 'SetName', player.getName());
  myGameInstance.SendMessage('Yandex', 'SetPhoto', player.getPhoto("medium"));
  },

SaveExtern: function (date) {
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
  },

  LoadExtern: function () {    
   player.getData().then(_date=> {
    const myJSON = JSON.stringify(_date);
       myGameInstance.SendMessage('GameManager', 'SetPlayerInfo', myJSON);
       window.alert("Data get");
   });
  },

  ShowAdv : function () {

    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          // some action after close
          myGameInstance.SendMessage('YandexAdv', 'UnPauseMusic');
        },
        onError: function(error) {
          // some action on error
          myGameInstance.SendMessage('YandexAdv', 'UnPauseMusic');
        }
    }
})
  },

  UnpauseMusic: function () {
  myGameInstance.SendMessage('YandexAdv', 'UnPauseMusic');
  },
  }); 