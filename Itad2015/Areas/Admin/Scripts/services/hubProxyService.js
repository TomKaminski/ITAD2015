(function () {
    'use strict';

    function hubProxyService($rootScope, $, signalRServer, appEmailService) {
        function signalRHubProxyFactory(serverUrl, hubName, startOptions) {

            var connection = $.hubConnection(signalRServer);
            var proxy = connection.createHubProxy(hubName);
            connection.start(startOptions).done(function () { });

            connection.disconnected(function () {
                setTimeout(function () {
                    connection.start(startOptions).done(function() {
                        invoke('connect', appEmailService.getEmail(), connection.id, 1);
                    });
                }, 5000);
            });

            return {
                on: function (eventName, callback) {
                    proxy.on(eventName, function (result) {
                        $rootScope.$apply(function () {
                            if (callback) {
                                callback(result);
                            }
                        });
                    });
                },
                off: function (eventName, callback) {
                    proxy.off(eventName, function (result) {
                        $rootScope.$apply(function () {
                            if (callback) {
                                callback(result);
                            }
                        });
                    });
                },
                invoke: function () {
                    var args = $.makeArray(arguments);
                    proxy.invoke.apply(proxy, args);
                },
                connection: connection
            };
        };
        return signalRHubProxyFactory;
    };

    angular.module('hubApp').factory('hubProxyService', hubProxyService);
})();
