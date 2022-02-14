import React from 'react';
import Box from '@mui/material/Box';
import Grid from '@material-ui/core/grid';
import Typography from '@mui/material/Typography';
import CustomizedDialogs from './dialog';
import Aboutmeform from './Aboutmeform';
import type {UpdateProf} from '../pages/Profiles/[pid]';



{/* About me component.*/}
const Aboutme = ({updateFunc, newProfile, logCheck, desc}: {updateFunc: VoidFunction, newProfile: UpdateProf,
     logCheck: boolean|null, desc: string}) => {  
    return (
        <Box p = {2} boxShadow={12} sx={{ width: '100%', backgroundColor: 'primary.light', border: 1}}>
            <Grid container> 
                <Grid item xs={11}>
                    <Typography variant="h6" gutterBottom>
                        About me
                    </Typography> 
                </Grid> 
                <Grid item xs={1}>
                    {logCheck &&
                        <CustomizedDialogs color="inherit" topmarg={0}>
                            <Aboutmeform profileData={newProfile} onSubmit={({aboutme})=>{
                                newProfile.aboutMe = aboutme;
                                updateFunc();
                            }}/>
                        </CustomizedDialogs>
                    } {/* if the user isn't logged in, then edit option dissappears */}
                </Grid>
                <Grid item xs={12}>
                    <Typography variant="subtitle2" gutterBottom style={{ wordWrap: "break-word" }}>
                        {desc}
                    </Typography> 
                </Grid>
            </Grid>
        </Box>     
    )
}
export default Aboutme;

