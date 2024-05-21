import { useAxios } from "../Axios/useAxios";
import axios from "../../Apis/openBanking"
import { useFillIncomeData } from './useFillIncomeData';

export const useGetIncomes = () => {
    const [fillData] = useFillIncomeData();
    const [response, error, loading, fetch] = useAxios({
        axiosInstance: axios,
        method: 'GET',
        url: '/participants'
    }, fillData);
    
    return [response, error, loading, fetch];
}