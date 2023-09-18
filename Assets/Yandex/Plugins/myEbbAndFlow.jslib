mergeInto(LibraryManager.library, {

  RateGame: function(){
    ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
  },

  SaveExtern: function(date){
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
    console.log("----------saved---------", myobj);
  },

  LoadExtern: function(){
    player.getData().then(_date =>{
      const myJSON = JSON.stringify(_date);
      myGameInstance.SendMessage('GameManager', 'SetPlayerInfo', myJSON);
      console.log("----------loded---------", myJSON);
    }); 
  },

  SetToLeaderboard : function(value){
    ysdk.getLeaderboards()
      .then(lb => {
        lb.setLeaderboardScore('Score', value);
      });
  },

  ShowAdv : function(){
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          console.log("------------- closed -------------");
          myGameInstance.SendMessage("GameManager", "UnPauseAudi");
          myGameInstance.SendMessage("GameManager", "AdvClosedMethod");
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
        } 
        })
  },

  AddExtraTimeExtern : function(){
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
          
        },
        onRewarded: () => {
          console.log('Rewarded!');
        },
        onClose: () => {
          console.log('Video ad closed.');
          myGameInstance.SendMessage("GameManager", "UnPauseAudi");
          myGameInstance.SendMessage("GameManager", "AddTime");
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
  },

  GetLang: function () {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
  },

});