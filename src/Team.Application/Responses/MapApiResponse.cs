using AutoWrapper;

namespace Team.Application.Responses
{
    public class MapApiResponse
    {
        [AutoWrapperPropertyMap(Prop.Result)]
        public object Data { get; set; }

        [AutoWrapperPropertyMap(Prop.ResponseException)]
        public object Error { get; set; }

        [AutoWrapperPropertyMap(Prop.StatusCode)]
        public string Code { get; set; }
    }
}
