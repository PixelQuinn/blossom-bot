using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Google.Apis.Discovery;
using static System.Net.Mime.MediaTypeNames;

public class SpellbookCommands : BaseCommandModule
{

    public class Spell
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
    }

    List<Spell> spells = new List<Spell>
    {
        // Level 1 Spells
        new Spell { Name = "Purrtection", Description = "Surround yourself with a soothing purr, granting advantage on Charisma saving throws and making you irresistible to feline creatures.", Level = 1 },
        new Spell { Name = "Catnip Bolt",  Description = "Hurl a magical bolt infused with the essence of catnip at a target, dealing 1d8 psychic damage. On a failed Wisdom saving throw, the target is distracted and has disadvantage on its next attack roll.", Level = 1 },
        new Spell { Name = "Mage Paw",  Description = "Transform the touched object into a small, harmless cat statue. While in this form, the object retains its properties but gains an adorable feline appearance.", Level = 1},
        new Spell { Name = "Landing on All Fours", Description = "When a creature falls, gently guide them to the ground as if they always land on their feet. The fall becomes slow and controlled, preventing fall damage.", Level = 1},
        new Spell { Name = "Furry Friends",  Description = "Summon a swarm of playful spectral cats to distract and harass enemies in the area. Creatures within the swarm's space must succeed on a Dexterity saving throw or take 1d6 slashing damage.", Level = 1},

        // Level 2 Spells
        new Spell { Name = "Telepawtation", Description = "Teleports the caster using kitty magic to a location they can see.",  Level = 2 },
        new Spell { Name = "Hissilient Image", Description = "Creates an illusory image of a hissing cat to distract and confuse enemies.", Level = 2 },
        new Spell { Name = "Clawmorph", Description = "Transform into a cat for a short duration, gaining increased agility and stealth. You can move through spaces as narrow as 1 inch wide without squeezing, and you have advantage on Dexterity (Stealth) checks.", Level = 2 },
        new Spell { Name = "Paws of Enfeeblement", Description = "Channel the weakness of a cat's touch to sap the strength of a creature within range. The target must succeed on a Constitution saving throw or have its Strength score reduced by 1d6 for the spell's duration.", Level = 2 },
        new Spell { Name = "Dopplepurr", Description = "Create an illusory duplicate of yourself that appears indistinguishable from you. The duplicate can move and mimic your actions, providing advantage on Dexterity saving throws until it is dispelled or takes damage.", Level = 2 },

        // Level 3 Spells
        new Spell { Name = "Whiskerwind Shield", Description = "Summons a protective shield made of magical whiskers that deflects attacks.", Level = 3 },
        new Spell { Name = "Catnap", Description = "Allows the caster and their allies to take a short rest in the blink of an eye.", Level = 3 },
        new Spell { Name = "Blinking Catwalk", Description = "Instantly teleport to an unoccupied space you can see within range, leaving behind an illusory image of a cat that confuses and distracts enemies. This teleportation does not provoke opportunity attacks.", Level = 3 },
        new Spell { Name = "Necro-purr-mancy", Description = "Raise a fallen feline ally as a spectral guardian. The cat companion has its own initiative and can make attacks, providing assistance to its allies. If it is reduced to 0 hit points, it vanishes, returning to the afterlife.", Level = 3 },
        new Spell { Name = "Purralysis Ray", Description = "Unleash a beam of purr energy at a target within range. On a hit, the target must succeed on a Constitution saving throw or be paralyzed for 1 minute. The target can repeat the saving throw at the end of each of its turns, ending the effect on a success.", Level = 3 },

        // Level 4 Spells
        new Spell { Name = "Dimensional Catnapping", Description = "Open a temporary portal to a cozy cat dimension. Allies within the area can use their reaction to step into the portal and take a short rest, gaining the benefits without spending any time.", Level = 4 },
        new Spell { Name = "Cat - astrophic Wave", Description = "Unleash a wave of magical energy in a cone, causing all creatures in its area to make a Dexterity saving throw.On a failed save, they take 6d6 force damage and are knocked prone.On a successful save, they take half damage and are not knocked prone.", Level = 4 },
        new Spell { Name = "Purrifying Flames", Description = "Ignite a target with magical flames that purify and heal. The target regains hit points equal to 4d8 + your spellcasting ability modifier, and any curses or diseases affecting the target are lifted.", Level = 4 },
        new Spell { Name = "Phantom Litter Box", Description = "Create an illusory litter box in an area. Enemies who enter the area must make a Wisdom saving throw or become frightened for 1 minute. A creature can repeat the saving throw at the end of each of its turns, ending the effect on a success.", Level = 4 },
        new Spell { Name = "Spectral Catwalk", Description = "Conjure a shimmering catwalk in the air. Allies can use their movement to walk along the catwalk without touching the ground, avoiding difficult terrain and gaining advantage on Dexterity (Stealth) checks while on the catwalk.", Level = 4 },

        // Level 5 Spells
        new Spell { Name = "Feline Familiar Swarm", Description = "Summon a swarm of spectral familiars in the form of various feline creatures. These familiars act as independent entities, each making attacks and providing assistance to the caster or their allies. The swarm disperses after 1 minute.", Level = 5 },
        new Spell { Name = "Cat's Grace", Description = "Channel the agility and grace of a cat, granting yourself or a willing creature you touch advantage on Dexterity saving throws and the ability to move across difficult terrain without taking damage or penalties for the duration.", Level = 5 },
        new Spell { Name = "Purrpetual Motion", Description = "Transform into an ethereal cat, granting yourself the ability to move through other creatures and objects as if they were difficult terrain. You can move through solid objects, and your attacks pass through armor and shields.", Level = 5 },
        new Spell { Name = "Whiskerstorm", Description = "Summon a magical storm of whiskers that swirls around a target area. Creatures within the storm must succeed on a Strength saving throw or be restrained for the spell's duration. A restrained creature can make a Strength saving throw at the end of each of its turns, ending the effect on a success.", Level = 5 },
        new Spell { Name = "Ninth Life", Description =  "Grant a target the blessing of multiple lives. If the target is reduced to 0 hit points within the spell's duration, they are instantly resurrected with half of their maximum hit points. This effect can only occur once during the spell's duration.", Level = 5 },

        // Level 6 Spells
        new Spell { Name = "Purrmanence", Description = "Grant a willing creature or yourself immunity to death and aging for the spell's duration. The recipient gains nine lives, allowing them to cheat death multiple times. Each time they would be reduced to 0 hit points, they instead regain hit points equal to half their maximum.", Level = 6 },
        new Spell { Name = "Cat-tastrophic Teleportation", Description = "Teleport to any location you can see, leaving behind a magical explosion of catnip. Creatures within the area must succeed on a Constitution saving throw or be charmed for 1 minute. Charmed creatures can repeat the saving throw at the end of each of their turns, ending the effect on a success.", Level = 6 },
        new Spell { Name = "Mystic Paw-rtal", Description = "Create a portal to a pocket dimension filled with catnip and cozy resting spots. Allies can use the portal to enter the pocket dimension and spend up to 1 hour resting, regaining hit points and spell slots as if they had completed a long rest.", Level = 6 },
        new Spell { Name = "Feline Frenzy", Description = "Summon a horde of spectral feline creatures to swarm over your enemies. Creatures in the area must succeed on a Wisdom saving throw or become frightened for the spell's duration. Frightened creatures can repeat the saving throw at the end of each of their turns, ending the effect on a success.", Level = 6 },
        new Spell { Name = "Ethereal Catnap", Description = "Extend the benefits of a short rest to all creatures within a 30-foot radius, regardless of the usual restrictions. This magical rest lasts for 10 minutes and allows creatures to regain hit points and abilities as if they had completed a short rest.", Level = 6 },

        // Level 7 Spells
        new Spell { Name = "Cat-tastrophic Annihilation", Description = "Unleash a wave of magical energy that sweeps over a large area, causing all creatures in its radius to make a Dexterity saving throw. On a failed save, they take 8d10 force damage and are knocked prone. On a successful save, they take half damage and are not knocked prone.", Level = 7 },
        new Spell { Name = "Seventh Life", Description = "Grant a target the ultimate blessing of multiple lives. The target gains an additional three lives, and when reduced to 0 hit points, they are instantly resurrected with their full maximum hit points. This effect can only occur three times during the spell's duration.", Level = 7 },
        new Spell { Name = "Ethereal Catwalk Mastery", Description = "Enhance the Ethereal Catwalk spell, allowing it to be cast at a higher level. Allies on the catwalk gain additional benefits, such as increased movement speed, advantage on attack rolls, and immunity to difficult terrain.", Level = 7 },
        new Spell { Name = "Purrfect Illusionist", Description = "Become the master illusionist, creating incredibly realistic and complex illusions of feline creatures. These illusions can interact with the environment and creatures, causing genuine reactions. The illusions can deal damage, distract enemies, or perform other actions within the limits of illusion magic.", Level = 7 },
        new Spell { Name = "Cataclysmic Transformation", Description = "Transform into a massive, powerful feline form, gaining increased strength, durability, and magical abilities. In this form, you have advantage on all saving throws, resistance to damage, and your attacks deal additional damage. The transformation lasts for 1 minute.", Level = 7 },

        // Level 8 Spells
        new Spell { Name = "Ethereal Catmastery", Description = "Become a master of the ethereal realm, gaining the ability to phase through objects and move effortlessly through the battlefield. Attacks pass harmlessly through you, and you can move through other creatures and obstacles without impediment for the spell's duration.", Level = 8 },
        new Spell { Name = "Purrvasive Invisibility", Description = "Grant yourself or a target creature invisibility for the spell's duration. Unlike regular invisibility, this spell also conceals the sounds of movement, making the invisible creature nearly impossible to detect by sight or sound.", Level = 8 },
        new Spell { Name = "Cat-astrophic Reckoning", Description = "Grant yourself or a target creature invisibility for the spell's duration. Unlike regular invisibility, this spell also conceals the sounds of movement, making the invisible creature nearly impossible to detect by sight or sound.", Level = 8 },
        new Spell { Name = "Feline Foresight", Description = "Enhance your senses to feline levels, gaining advantage on all Wisdom-based checks, as well as a supernatural awareness of danger. You can't be surprised, and attacks against you have disadvantage for the spell's duration.", Level = 8 },
        new Spell { Name = "Temporal Catnap", Description = "Create a temporal bubble in which time flows differently. Allies within the bubble can take a short rest that lasts only 1 minute but provides all the benefits of a long rest. The temporal bubble lasts for 1 hour.", Level = 8 },

        // Level 9 Spells
        new Spell { Name = "Purrfect Ascendance", Description = "Ascend to a higher plane of existence, becoming an ethereal feline entity. In this form, you gain immunity to all damage, the ability to move through any barriers, and the power to manipulate reality within the limits of the spell's duration.", Level = 9 },
        new Spell { Name = "Cataclysmic Catastrophe", Description = "Unleash the ultimate magical cataclysm, summoning a storm of destructive cat energy that envelops the entire battlefield. All creatures in the area must make a Constitution saving throw or suffer 10d10 force damage and be petrified. On a successful save, they take half damage and are not petrified.", Level = 9 },
        new Spell { Name = "Eternal Catnap", Description = "Create a pocket dimension where time stands still. Allies within the dimension can take a short rest that lasts for 1 hour but provides all the benefits of a long rest. The pocket dimension remains in existence for 8 hours, allowing multiple groups to benefit from its effects.", Level = 9 },
        new Spell { Name = "Ninth Life's Blessing", Description = "Grant a target creature the blessing of an additional nine lives, making them virtually immortal. If the target is reduced to 0 hit points, they are instantly resurrected with full hit points, and this effect can occur up to nine times during the spell's duration.", Level = 9 },
        new Spell { Name = "Ultimate Cat-astrophe", Description = "Invoke the ultimate cat-astrophe upon your enemies. All creatures in a massive area must make a Wisdom saving throw or be overwhelmed by a flood of magical cat energy, suffering various debilitating effects. The specifics of the effects are left to the whims of the feline magic, making each casting unique.", Level = 9 },

        // Big bad boss Spells
        new Spell { Name = "Feline Sovereignty", Description = "Unleash the true power of the Cat Lord, establishing dominion over the battlefield. All creatures within 60 feet must make a Wisdom saving throw or fall under your control for 1 minute. Charmed creatures see you as the ultimate authority, obeying your every command without question.", Level = 99 },
        new Spell { Name = "Purrpetual Nightfall", Description = "Summon the eternal night with a powerful feline incantation. Darkness envelopes the battlefield, and all sources of light are extinguished. Only those with darkvision or other means of seeing in darkness can perceive anything within this magical nightfall.", Level = 99 },
        new Spell { Name = "Cataclysmic Roar", Description = "Release a deafening roar that reverberates through the very fabric of reality. All creatures within a 30-foot cone must make a Constitution saving throw or suffer 12d8 thunder damage and be stunned for 1 minute. On a successful save, they take half damage and are not stunned.", Level = 99 },
        new Spell { Name = "Necro-purr-mancer's Grasp", Description = "Summon the spectral hands of the Cat Lord to reach into the ethereal realm and grasp the life force of a target creature. The target must succeed on a Constitution saving throw or take 15d6 necrotic damage. On a successful save, they take half damage.", Level = 99 },
        new Spell { Name = "Fury of the Nine Lives", Description = "Channel the cumulative power of nine lives into a devastating magical onslaught. All creatures in a 40-foot radius must make a Dexterity saving throw or suffer 10d10 force damage, and their movement speed is halved for 1 minute. On a successful save, they take half damage and are not affected by reduced movement speed.", Level = 99 }
    };

    string[] catchphrases = { "Mrowl! I am the master of magic!", "Hiss! Tremble before my arcane might!" };
    string[] castingPhrases = { "Abracadabra!", "Meowgicus!", "Purrformus!" };

    [Command("spellbook")]
    [Description("Consult the cat wizard's spellbook for arcane knowledge.")]
    public async Task SpellbookCommand(CommandContext ctx, [RemainingText] string target = null)
    {
        // Delete the command message
        await ctx.Message.DeleteAsync();

        Random random = new Random();

        // Pick a random spell
        Spell randomSpell = spells[random.Next(spells.Count)];

        // Pick a random catchphrase
        string randomCatchphrase = catchphrases[random.Next(catchphrases.Length)];

        // Pick a random casting phrase
        string randomCastingPhrase = castingPhrases[random.Next(castingPhrases.Length)];

        // Construct the message with catchphrase
        string message = $"*Evil Cat Wizard Catchphrase:* {randomCatchphrase}";

        // Send the catchphrase
        var catchphraseMessage = await ctx.RespondAsync(message);

        // Pause for 2 seconds
        await Task.Delay(2000);

        // Construct the message with casting phrase
        message = $"*Casting Phrase:* {randomCastingPhrase}";

        // Edit the catchphrase message to include casting phrase
        await catchphraseMessage.ModifyAsync(message);

        // Pause for 2 seconds
        await Task.Delay(2000);

        // Construct the final spell casting message
        message = $"**Blossom casts {randomSpell.Name}**";

        // Include target information if specified
        if (!string.IsNullOrEmpty(target))
        {
            message += $" @ {target}";
        }

        // Send the final spell casting message
        await ctx.RespondAsync(message);
    }


}
