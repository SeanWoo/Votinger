import { compose, createStore } from "redux";
import reducers from './reducers';


// @ts-ignore
const reduxStore = createStore(reducers, window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__())

export default reduxStore;