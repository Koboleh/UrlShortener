import './App.css';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import LoginPage from "./pages/LoginPage/LoginPage";
import UrlsListPage from "./pages/UrlsListPage/UrlsListPage";
import UrlPage from "./pages/UrlPage/UrlPage";

function App() {

  const router = createBrowserRouter([
    {
      path: "/",
      element: <UrlsListPage />,
    },
    {
      path: "/urls/:id",
      element: <UrlPage />,
    },
    {
      path: "/login",
      element: <LoginPage />,
    }
  ]);
  return (
      <div className="App">
        <RouterProvider router={router}/>
      </div>
  );
}

export default App;
