import {
    Navigate,
    RouteProps,
} from 'react-router-dom';
import auth from '../middlewares/auth';

export function PrivateRoute({ children }: RouteProps): JSX.Element {
    const isLoggedIn = auth()
    return (
        <>
            {isLoggedIn
                ? children
                : <Navigate to="/" />
            }
        </>
    );
}