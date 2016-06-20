(function (angular) {
    angular
        .module('settlementModule')
        .filter('trustAsHtml', trustAsHtml);

    trustAsHtml.$inject = ['$sce'];

    function trustAsHtml ($sce) {
        return function (input) {
            return $sce.trustAsHtml(input);
        }
    }

})(angular);