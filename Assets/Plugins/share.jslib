mergeInto(LibraryManager.library, {
  ShareFuncsiaTelegram: function (moneu, distansion) {
    window.open(
      "https://telegram.me/share/url?url=" + textMesends(moneu, distansion),
      "sharer",
      "status=0,toolbar=0,width=650,height=500"
    );
  },
  ShareFuncsiaFacebook: function (moneu, distansion) {
    window.open(
      "https://www.facebook.com/sharer.php?u=" + textMesends(moneu, distansion),
      "sharer",
      "status=0,toolbar=0,width=650,height=500"
    );
  },
  ShareFuncsiaTwitter: function (moneu, distansion) {
    window.open(
      "https://twitter.com/intent/tweet?text=" + textMesends(moneu, distansion),
      "sharer",
      "status=0,toolbar=0,width=650,height=500"
    );
  },
  registor: function (moneu, distansion) {
    conect();
  },
  buiNftJS: function () {
    return StartbuiNft();
  },
});
