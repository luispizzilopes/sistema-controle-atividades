import { CircularProgress } from "@mui/material"
import './styles/loadingProgress.css'

interface ILoadingProgress {
    loading: boolean;
}

export default function LoadingProgress({ loading }: ILoadingProgress) {
    return (
        <div>
            {loading ?
                <div className="loading-progress">
                    <div>
                        <CircularProgress />
                    </div>
                </div>
                :
                <></>
            }
        </div>
    );
}