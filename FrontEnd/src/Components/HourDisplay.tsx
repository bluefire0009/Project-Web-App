export const HourDisplay: React.FC = () => {
        return<div className="HourDisplay">
            <div style={{textAlign:"center",paddingTop:"130%"}}>0:00</div>
            {Array.from({ length: 23 }, (_, i) => i+1 ).map((hour) => 
            <div className="HourText">
                {hour}:00
            </div>)}
            <div style={{textAlign:"center", marginTop:"85%"}}>24:00</div>
        </div>
}