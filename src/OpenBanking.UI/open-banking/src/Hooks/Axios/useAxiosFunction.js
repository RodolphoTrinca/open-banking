import {useState, useEffect} from "react";

export const useAxiosFunction = (transformResponse) => {
    const [response, setResponse] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    const [controller, setController] = useState();

    const axiosFetch = async (configObject, callback) => {
        const {
            axiosInstance,
            url,
            method,
            data,
            requestConfig = {}
        } = configObject;

        try {
            setLoading(true);

            const ctrl = new AbortController();
            setController(ctrl);
            
            const res = await axiosInstance.request({
                ...requestConfig,
                signal: ctrl.signal,
                url: url,
                method: method.toLowerCase(),
                data: data
            });

            if(res.status > 199 && res.status < 300)
            {
                const data = transformResponse != undefined ? transformResponse(res.data) : res.data;
                setResponse(data);
                
                callback && callback();
                return;
            }
            
            setError(res.data);
        } catch (err) {
            console.error(err, err.stack);
            setError(err.message);
        }finally{
            setLoading(false);
        }
    }

    useEffect(() => {
        return () => controller && controller.abort();
    }, [controller]);

    return [response, error, loading, axiosFetch];
}