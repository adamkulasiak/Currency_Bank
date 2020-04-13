import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import MainPage from "./views/MainPage";
import Login from "./views/Login";
import MainBar from "./components/MainBar";

export default function App() {
  return (
    <div className="App">
      <Router>
        <MainBar />
        <Switch>
          <Route path="/" component={MainPage} exact />
          <Route path="/login" component={Login} />
        </Switch>
      </Router>
    </div>
  );
}
