using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Model;

namespace EventManagement.Services
{
    public class AttendeeService: IAttendeeService
    {
        private readonly IUploadHandler<Attendee> _attendeeUploadHandler;
        private readonly IEventManagementService<Attendee> _eventManagementService;

        public AttendeeService(IUploadHandler<Attendee> attendeeUploadHandler, IEventManagementService<Attendee> eventManagementService)
        {
            _attendeeUploadHandler = attendeeUploadHandler;
            _eventManagementService = eventManagementService;
        }

        public async Task<List<Attendee>> CreateAttendeeAsync(IFormFile file)
        {
            try
            {
                string pathToFile = _attendeeUploadHandler.UploadFile(file);
                List<Attendee> attendees = await _attendeeUploadHandler.ReadDetailsFromExcel(pathToFile);

                List<Attendee> failedAttendees = new List<Attendee>();

                foreach (Attendee attendee in attendees)
                {
                    // Check if user exists
                    // System.Console.WriteLine(attendee.UserId);
                    bool userExists = await _eventManagementService.UserExistsAsync(attendee.UserId);
                    if (!userExists)
                    {
                        Console.WriteLine($"User with ID {attendee.UserId} does not exist. Skipping.");
                        failedAttendees.Add(attendee);
                        continue;
                    }
                    bool eventExists = await _eventManagementService.EventExistsAsync(attendee.EventId);
                    if (!eventExists)
                    {
                        Console.WriteLine($"Event with ID {attendee.EventId} does not exist. Skipping.");
                        failedAttendees.Add(attendee);
                        continue;
                    }
                    bool conflict = await _eventManagementService.GetConflictsAsync(attendee.EventId, attendee.UserId, attendee.EventDate);
                    if (conflict)
                    {
                        Console.WriteLine($"Conflict: User with ID {attendee.UserId} is already booked for another event on {attendee.EventDate}. Skipping.");
                        failedAttendees.Add(attendee);
                        continue;
                    }
                    
                    await _eventManagementService.CreateAsync(attendee);
                }

                _attendeeUploadHandler.DeleteFile(pathToFile);
                Console.WriteLine($"Failed to create {failedAttendees.Count} attendees.");

                return failedAttendees;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during CreateAttendeeAsync: {ex.Message}");
                throw new InvalidOperationException("Error creating attendees. See logs for details.", ex);
            }
        }


        public async Task<IEnumerable<Attendee>> GetAllAttendeesAsync()
        {
            try
            {
                return await _eventManagementService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAllAttendeesAsync: {ex.Message}");
                throw new InvalidOperationException("Error retrieving all attendees. See logs for details.", ex);
            }
        }

        public async Task DeleteAllAttendeesAsync()
        {
            try
            {
                await _eventManagementService.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during DeleteAllAttendeesAsync: {ex.Message}");
                throw new InvalidOperationException("Error deleting all attendees. See logs for details.", ex);
            }
        }
        
    }
}