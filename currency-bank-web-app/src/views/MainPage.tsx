import * as React from "react";
import { Grid, Container } from "@material-ui/core";
import { connect } from "react-redux";
import { State } from "../_reducers";
import { IUser } from "../interfaces/login/IUser";
import DataTable from "../components/DataTable";

interface IMainPageProps {
  user: IUser | null;
  username: string | null;
}

function MainPage(props: IMainPageProps) {
  return (
    <Container>
      <Grid container>
        <Grid item>
          <DataTable accounts={props.user?.accounts} />
        </Grid>
      </Grid>
    </Container>
  );
}

function mapStateToProps(state: State) {
  const { user, username } = state.authentication;
  return {
    user,
    username,
  };
}

export default connect(mapStateToProps)(MainPage);
