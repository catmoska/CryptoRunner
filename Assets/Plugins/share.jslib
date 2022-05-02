function textMesends(moneu,distansion)
      {
        return "fff"+moneu+" "+distansion;
      }

      mergeInto(LibraryManager.library, {

          ShareFuncsiaTelegram : function (moneu,distansion) {
              window.open('https://telegram.me/share/url?url='+textMesends(moneu,distansion),'sharer','status=0,toolbar=0,width=650,height=500');
          },
          ShareFuncsiaFacebook : function (moneu,distansion) {
              window.open('https://www.facebook.com/sharer.php?u='+textMesends(moneu,distansion),'sharer','status=0,toolbar=0,width=650,height=500');
          },
          ShareFuncsiaTwitter : function (moneu,distansion) {
              window.open('https://twitter.com/intent/tweet?text='+textMesends(moneu,distansion),'sharer','status=0,toolbar=0,width=650,height=500');
          },
          registor : function (moneu,distansion) {
              conect();
          },
      });

      const url = "{% url 'registr'%}"
        async function conect() {
            try {
                const resp = await window.solana.connect();
                p = resp.publicKey.toString();
                // console.log(p);
                await getData('POST', url, {
                    'id': p
                });
                window.location.href = "{% url 'geim'%}";
            } catch (err) {
                window.open("https://phantom.app/", "_blank");
            }
        }

        async function getData(method, url, body = null) {
            return new Promise((resolve, reject) => {
                const xhr = new XMLHttpRequest()

                xhr.open(method, url)

                xhr.responseType = 'json'
                xhr.setRequestHeader('Content-Type', 'application/json')

                xhr.onload = () => {
                    if (xhr.status >= 400)
                        reject(xhr.response)
                    else
                        resolve(xhr.response)
                }

                xhr.onerror = () => {
                    reject(xhr.response)
                }

                xhr.send(JSON.stringify(body))
            })
        }