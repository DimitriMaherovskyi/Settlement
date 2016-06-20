(function (angular) {
    angular.module('settlementModule').directive('autoHeight', function () {
        return {
            restrict: 'A',
            link: function (scope, elem, attrs) {
                elem.css('min-height', (attrs.autoHeight * 10) + 200 + 'px');
            }
        };
    });
})(angular);