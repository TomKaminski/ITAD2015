angular.module('hubApp', [])
    .value('signalRServer', '')
    .constant('$', window.jQuery);

angular.module('adminApp', ['hubApp']);
