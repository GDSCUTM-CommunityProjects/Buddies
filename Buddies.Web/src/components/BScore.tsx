import React from 'react';
import Card from '@mui/material/Card';
import Grid from '@material-ui/core/Grid';
import Typography from '@mui/material/Typography';
import Button from '@material-ui/core/Button';

/* Buddy Score component. */
const BScore = ({ score }: { score: number }) => {
  return (
    <Card sx={{
      padding: 2,
      width: '100%',
      height: 60,
      border: 1,
      alignItems: 'center',
      paddingRight: 3,
      boxShadow: 12,
    }}
    >
      <Grid container>
        <Grid item xs={11}>
          <Typography color="inherit" variant="h6" gutterBottom>
            Buddy Score
          </Typography>
        </Grid>
        <Grid item xs={1}>
          <Button
            variant="contained"
            style={{
              color: 'white',
              backgroundColor: 'green',
              maxWidth: '30px',
              maxHeight: '30px',
              minWidth: '30px',
              minHeight: '30px',
            }}
          >
            {score}
          </Button>
        </Grid>
      </Grid>
    </Card>
  );
};
export default BScore;
